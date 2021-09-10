using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCamera.Classes.CameraAnimation;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private bool _IsReignSinceLastTick = false;



		////////////////

		private void UpdateReignPerTick( bool isReign ) {
			if( isReign ) {
				if( !this._IsReignSinceLastTick ) {
					Main.NewText( "A fell presence can be felt. A powerful, unconquered entity now reigns in your world.", Color.OrangeRed );
				}
			} else {
				if( this._IsReignSinceLastTick ) {
					Main.NewText( "The fell presence has vanished... for now.", new Color( 175, 75, 255 ) );
				}
			}

			if( isReign ) {
				if( this.player.townNPCs == 0 ) {
					if( this.player.chest >= 0 ) {
						this.player.chest = -1;

						Main.NewText( "Dark forces of the land seal this chest.", Color.Yellow );
					}
				}

				if( BossReignsMod.Instance.NecrotisMod != null ) {
					BossReignsPlayer.UpdateReignForNecrotis_WeakRef( this.player );
				}

				this.UpdateReignFx();
			}

			//

			this._IsReignSinceLastTick = isReign;
		}

		private void UpdateReignPer5Ticks( bool isReign ) {
			if( ModLoader.GetMod("CursedBrambles") != null ) {
				BossReignsPlayer.UpdateReignForCursedBrambles_WeakRef( this.player, isReign );
			}
		}


		////////////////

		private void UpdateReignFx() {
			if( this.ShakeIntermissionTimer-- <= 0 ) {
				this.ShakeIntermissionTimer = Main.rand.Next( 60 * 15, 60 * 60 );

				if( CameraShaker.Current == null || !CameraShaker.Current.IsAnimating() ) {
					CameraShaker.Current = new CameraShaker(
						name: "BossReignQuakes",
						peakMagnitude: 0.5f + Main.rand.NextFloat(),
						toDuration: 60,
						lingerDuration: 60,
						froDuration: 60,
						isSmoothed: true
					);
				}
			}
		}
	}
}