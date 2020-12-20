using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Mods.BossChecklist;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
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
	}
}
