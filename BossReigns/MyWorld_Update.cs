using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Mods.BossChecklist;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		private void UpdateBossDownedCheck() {
//DebugHelpers.Print( "downed", string.Join( ", ",
//	BossChecklistService.BossInfoTable
//	.Select(kv => kv.Key+":"+kv.Value.IsDowned())
//) );
			foreach( (string boss, BossChecklistService.BossInfo info) in BossChecklistService.BossInfoTable ) {
				if( info.IsDowned() ) {
					if( !this.DownedBossesSnapshot.Contains( boss ) ) {
						this.RegisterBossKill( boss );
					}
				}
			}
		}

		private void UpdateReignBuildup() {
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
