using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Mods.BossChecklist;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		public int ElapsedPresenceTicks { get; internal set; }

		////

		private bool IsLoaded = false;

		private ISet<string> DownedBossesSnapshot = new HashSet<string>();



		////////////////

		public override void Initialize() {
			this.ElapsedPresenceTicks = 0;
			this.DownedBossesSnapshot.Clear();

			this.IsLoaded = false;
		}

		////

		public override void Load( TagCompound tag ) {
			this.DownedBossesSnapshot.Clear();

			var config = BossReignsConfig.Instance;

			if( tag.ContainsKey("elapsed_ticks") ) {
				this.ElapsedPresenceTicks = tag.GetInt( "elapsed_ticks" );
			} else {
				this.ElapsedPresenceTicks = -config.Get<int>( nameof(config.AddedTicksUntilFirstReign) );

				LogHelpers.Log( "Added to timer for initial boss reign." );
			}

			if( config.DebugModeFastTime ) {
				this.ElapsedPresenceTicks /= 60;
			}

			if( tag.ContainsKey("bc_snapshot_count") ) {
				int bcListCount = tag.GetInt( "bc_snapshot_count" );

				for( int i=0; i<bcListCount; i++ ) {
					string boss = tag.GetString( "bc_snapshot_boss_"+i );
					this.DownedBossesSnapshot.Add( boss );
				}
			}

			this.IsLoaded = true;
		}

		public override TagCompound Save() {
			int bcListCount = this.DownedBossesSnapshot.Count;
			var tag = new TagCompound {
				{ "elapsed_ticks", this.ElapsedPresenceTicks },
				{ "bc_snapshot_count", bcListCount }
			};

			int i = 0;
			foreach( string boss in this.DownedBossesSnapshot ) {
				tag["bc_snapshot_boss_" + i] = boss;
				i++;
			}

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.ElapsedPresenceTicks = reader.ReadInt32();
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.ElapsedPresenceTicks );
			} catch { }
		}


		////////////////

		 private int _BossCheckTimer = 0;

		public override void PreUpdate() {
			if( !this.IsLoaded ) {
				return;
			}

			this.UpdateReignBuildup();

			if( this._BossCheckTimer++ > 15 ) {
				this._BossCheckTimer = 0;
				this.UpdateBossDownedCheck();
			}
		}


		////////////////

		private void UpdateBossDownedCheck() {
			foreach( (string boss, BossChecklistService.BossInfo info) in BossChecklistService.BossInfoTable ) {
				if( !info.IsDowned() ) {
					continue;
				}

				if( !this.DownedBossesSnapshot.Contains(boss) ) {
					this.RegisterBossKill( boss );
				}
			}
		}

		private void UpdateReignBuildup() {
			var config = BossReignsConfig.Instance;
			int minTicks = config.Get<int>( nameof(config.LowestAmountTicksBeforeReign) );
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );

			if( config.DebugModeFastTime ) {
				minTicks /= 60;
				maxTicks /= 60;
			}

			if( this.ElapsedPresenceTicks < minTicks ) {
				this.ElapsedPresenceTicks = minTicks;
			}

			this.ElapsedPresenceTicks++;
			if( this.ElapsedPresenceTicks > maxTicks ) {
				this.ElapsedPresenceTicks = maxTicks;
			}
		}


		////////////////

		public void RegisterBossKill( string boss ) {
			this.DownedBossesSnapshot.Add( boss );

			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );
			int subTicks = config.Get<int>( nameof( config.TicksRemovedFromEachBossKill ) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
				subTicks /= 60;
			}

			this.ElapsedPresenceTicks -= subTicks;

			if( config.DebugModeInfo ) {
				Main.NewText( "Boss "+boss+" kill detected. Ticks until reign: "
					+this.ElapsedPresenceTicks+" of "+maxTicks );
			}
		}
	}
}
