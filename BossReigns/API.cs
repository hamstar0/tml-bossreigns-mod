using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


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
	}
}
