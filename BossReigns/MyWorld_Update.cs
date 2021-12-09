using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsInterMod.Services.Mods.BossChecklist;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		private void UpdateBossDownedCheck() {
//DebugHelpers.Print( "downed", string.Join( ", ",
//	BossChecklistService.BossInfoTable
//	.Select(kv => kv.Key+":"+kv.Value.IsDowned())
//) );
			this.ViableBossSummonItems.Clear();

			foreach( (string boss, BossChecklistService.BossInfo info) in BossChecklistService.BossInfoTable ) {
				if( info.IsBoss && !info.IsMiniboss ) {
					if( info.IsDowned() ) {
						if( !this.DownedBossesSnapshot.Contains( boss ) ) {
							this.RegisterBossKill( boss );
						}
					} else {
						this.ViableBossSummonItems.UnionWith( info.SpawnItemTypes );
					}
				}
			}
		}

		private void UpdateReignBuildupIf() {
			if( !this.IsLoaded ) {
				return;
			}

			if( this.IsPaused ) {
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
