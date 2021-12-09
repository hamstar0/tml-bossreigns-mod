using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using BossReigns.Net;


namespace BossReigns {
	public class BossReignsAPI {
		public static float GetBackgroundPKEPercent() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			double elapsedTicks = (double)myworld.ElapsedReignBuildupTicks;

			var config = BossReignsConfig.Instance;
			double maxTicks = (double)config.Get<int>( nameof( config.TicksUntilReign ) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60d;
			}

			return MathHelper.Clamp( (float)( elapsedTicks / maxTicks ), 0f, 1f );
		}


		////

		public static bool IsReignBuildupPaused() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			return myworld.IsPaused;
		}


		public static void SetReignBuildupPause( bool isPaused ) {
			var myworld = ModContent.GetInstance<BossReignsWorld>();

			myworld.IsPaused = isPaused;

			if( Main.netMode == NetmodeID.Server ) {
				ReignBuildupPauseProtocol.SendToAllClients( isPaused );
			} else {
				LogLibraries.Warn( "Not server." );
			}
		}
	}
}
