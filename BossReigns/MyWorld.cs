using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		public ISet<int> ViableBossSummonItems { get; private set; } = new HashSet<int>();

		////

		public int ElapsedReignBuildupTicks { get; internal set; }


		////////////////

		private bool IsLoaded = false;

		private ISet<string> DownedBossesSnapshot = new HashSet<string>();



		////////////////

		public override void Initialize() {
			this.ElapsedReignBuildupTicks = 0;
			this.DownedBossesSnapshot.Clear();

			this.IsLoaded = false;
		}

		////

		public override void Load( TagCompound tag ) {
			this.DownedBossesSnapshot.Clear();

			var config = BossReignsConfig.Instance;

			if( tag.ContainsKey("elapsed_ticks") ) {
				this.ElapsedReignBuildupTicks = tag.GetInt( "elapsed_ticks" );
			}

			if( config.DebugModeFastTime ) {
				this.ElapsedReignBuildupTicks /= 60;
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
			var tag = new TagCompound {
				{ "elapsed_ticks", this.ElapsedReignBuildupTicks },
				{ "bc_snapshot_count", this.DownedBossesSnapshot.Count }
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
				this.ElapsedReignBuildupTicks = reader.ReadInt32();
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.ElapsedReignBuildupTicks );
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

		public override void PostWorldGen() {
			var config = BossReignsConfig.Instance;
			int addedTicks = config.Get<int>( nameof(config.AddedTicksUntilFirstReign) );

			if( addedTicks > 0 ) {
				this.ElapsedReignBuildupTicks = -addedTicks;
				LogLibraries.Log( "Added " + addedTicks + " ticks to timer for initial boss reign." );
			}
		}
	}
}
