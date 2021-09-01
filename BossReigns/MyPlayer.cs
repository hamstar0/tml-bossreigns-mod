using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private bool IsReignSinceLastTick = false;

		private int ReignTimer = 0;



		////////////////

		public override void PreUpdate() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			bool isReign = myworld.IsReign();

			if( this.ReignTimer-- == 0 ) {
				this.ReignTimer = 5;
				this.UpdateReignPer5Ticks( isReign );
			}

			this.UpdateReignPerTick( isReign );

			this.IsReignSinceLastTick = isReign;
		}


		////////////////

		private void UpdateReignPerTick( bool isReign ) {
			if( isReign ) {
				if( this.player.townNPCs == 0 ) {
					if( this.player.chest >= 0 ) {
						this.player.chest = -1;

						Main.NewText( "Dark forces of the land seal this chest.", Color.Yellow );
					}
				}
			}
		}

		private void UpdateReignPer5Ticks( bool isReign ) {
			if( isReign ) {
				if( !this.IsReignSinceLastTick ) {
					Main.NewText( "A fell presence can be felt. A powerful, unconquered entity now reigns in your world.", Color.OrangeRed );
				}
			} else {
				if( this.IsReignSinceLastTick ) {
					Main.NewText( "The fell presence has vanished... for now.", new Color(175, 75, 255) );
				}
			}

			if( ModLoader.GetMod("CursedBrambles") != null ) {
				BossReignsPlayer.UpdateReignForCursedBrambles_WeakRef( this.player, isReign );
			}
		}
	}
}