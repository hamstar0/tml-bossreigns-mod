using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private bool IsReign = false;

		private int ReignTimer = 0;



		////////////////

		public override void PreUpdate() {
			string timerName = "BossReignsBrambles_" + this.player.whoAmI;

			if( this.ReignTimer-- == 0 ) {
				this.ReignTimer = 5;
				this.UpdateReignPer5Ticks();
			}
		}


		////////////////

		private void UpdateReignPer5Ticks() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
			}

			bool isReign = myworld.ElapsedReignBuildupTicks >= maxTicks;

			if( isReign ) {
				if( !this.IsReign ) {
					Main.NewText( "Waves of darkness begin emanating from the land. A powerful new entity now reigns in your world.", Color.OrangeRed );
				}
			} else {
				if( this.IsReign ) {
					Main.NewText( "Waves of darkness subside... for now.", new Color(175, 75, 255) );
				}
			}

			this.IsReign = isReign;

			if( ModLoader.GetMod("CursedBrambles") != null ) {
				BossReignsPlayer.UpdateReignCursedBrambles( this.player, isReign );
			}
		}
	}
}