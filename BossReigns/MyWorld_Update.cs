using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsInterMod.Services.Mods.BossChecklist;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		public static bool IsGatewayBossAlone( int bossNpcType ) {
			BossChecklistService.BossInfo gateBossInfo = BossChecklistService.BossInfoTable
				.Values
				.First( i => i.SegmentNpcIDs.Contains(bossNpcType) );

			if( gateBossInfo.IsDowned() ) {
				return false;
			}

			//

			bool lesserBossesRemain = BossChecklistService.BossInfoTable
				.Values
				.Any( i => i.IsBoss && i.ProgressionValue < gateBossInfo.ProgressionValue && !i.IsDowned() );

			return !lesserBossesRemain;
		}



		////////////////

		private void UpdateBossDownedStatesSnapshot() {
//DebugHelpers.Print( "downed", string.Join( ", ",
//	BossChecklistService.BossInfoTable
//	.Select(kv => kv.Key+":"+kv.Value.IsDowned())
//) );
			this.ViableBossSummonItems.Clear();

			foreach( (string boss, BossChecklistService.BossInfo info) in BossChecklistService.BossInfoTable ) {
				if( info.IsBoss && !info.IsMiniboss ) {
					if( info.IsDowned() ) {
						if( !this.DownedBossesSnapshot.Contains(boss) ) {
							this.RegisterBossKill( boss );
						}
					} else {
						this.ViableBossSummonItems.UnionWith( info.SpawnItemTypes );
					}
				}
			}
		}


		private void UpdateReignBuildup_If() {
			if( !this.IsLoaded ) {
				return;
			}

			if( this.IsPaused ) {
				return;
			}

			//

			// Avoids potential Wall of Flesh softlock
			if( BossReignsWorld.IsGatewayBossAlone(NPCID.WallofFlesh) ) {
				return;
			}
			// Avoids potential Moon Lord softlock
			if( BossReignsWorld.IsGatewayBossAlone(NPCID.MoonLordHead) ) {  //NPCID.MoonLordCore
				return;
			}

			//

			var config = BossReignsConfig.Instance;
			int minTicks = config.Get<int>( nameof(config.MinimumTicksElapsed) );
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );

			if( config.DebugModeFastTime ) {
				minTicks /= 60;
				maxTicks /= 60;
			}

			if( this.ElapsedReignBuildupTicks < minTicks ) {
				this.ElapsedReignBuildupTicks = minTicks;
			}

			this.ElapsedReignBuildupTicks++;

			if( this.ElapsedReignBuildupTicks > maxTicks ) {
				this.ElapsedReignBuildupTicks = maxTicks;
			}
		}
	}
}
