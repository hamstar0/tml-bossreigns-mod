using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using CursedBrambles;


namespace BossReigns {
	class BossReignsPlayer : ModPlayer {
		public override void PreUpdate() {
			string timerName = "BossReignsBrambles_" + this.player.whoAmI;

			if( Timers.GetTimerTickDuration( timerName ) == 0 ) {
				Timers.SetTimer( timerName, 5, false, () => {
					return this.UpdateReign();
				} );
			}
		}


		////////////////

		private bool UpdateReign() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );
			bool isReign = myworld.ElapsedPresenceTicks >= maxTicks;

			if( isReign ) {
				CursedBramblesAPI.SetPlayerToCreateBrambleWake( this.player, 64, 15 );
			} else {
				CursedBramblesAPI.UnsetPlayerToCreateBrambleWake( this.player );
			}

			return isReign;
		}
	}
}