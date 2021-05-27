using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;


namespace BossReigns {
	partial class BossReignsWorld : ModWorld {
		public static bool IsViableSummonItem( int itemType ) {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			if( !myworld.ViableBossSummonItems.Contains( itemType ) ) {
				return false;
			}

			return myworld.IsReign();
		}



		////////////////

		public bool IsReign() {
			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof( config.TicksUntilReign ) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
			}

			return this.ElapsedReignBuildupTicks >= maxTicks;
		}


		////

		public void RegisterBossKill( string boss ) {
			this.DownedBossesSnapshot.Add( boss );

			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );
			int subTicks = config.Get<int>( nameof(config.TicksRemovedFromEachBossKill) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
				subTicks /= 60;
			}

			if( this.ElapsedReignBuildupTicks > (int)((float)maxTicks * 0.75f) ) {
				Timers.SetTimer( 60, true, () => {
					Main.NewText(
						"Powerful entity defeated. Ambient spiritual energy buildup subsides... for now.",
						new Color( 50, 130, 255 )
					);
					return false;
				} );
			}

			this.ElapsedReignBuildupTicks -= subTicks;

			if( config.DebugModeInfo ) {
				Main.NewText( "Boss "+boss+" kill detected. Ticks until reign: "
					+this.ElapsedReignBuildupTicks+" of "+maxTicks );
			}
		}
	}
}
