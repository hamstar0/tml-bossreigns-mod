using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		private static float LastGaugedBackgroundPKEPercent = 0f;



		////////////////

		public static void InitializePKE() {
			PKEMeter.Logic.PKEGaugesGetter gauge = PKEMeter.PKEMeterAPI.GetGauge();

			int gaugeTimer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				PKEMeter.Logic.PKEGaugeValues existingGauge = gauge?.Invoke( plr, pos )
					?? new PKEMeter.Logic.PKEGaugeValues( 0f, 0f, 0f, 0f);

				// Update gauge every 1/6s
				if( gaugeTimer-- <= 0 ) {
					gaugeTimer = 10;
					BossReignsMod.LastGaugedBackgroundPKEPercent = BossReignsAPI.GetBackgroundPKEPercent();
				}

				existingGauge.RedRealPercent = BossReignsMod.LastGaugedBackgroundPKEPercent;   // Red channel
				existingGauge.RedSeenPercent = existingGauge.RedRealPercent;
				existingGauge.IsRedInsignificant = true;

				return existingGauge;
			} );

			PKEMeter.PKEMeterAPI.SetMeterText( PKEMeter.Logic.PKEGaugeType.Red, ( plr, pos, gauges ) => {
				return new PKEMeter.Logic.PKETextMessage(
					message: "WARNING - CLASS V+ PKE-EMITTING ENTITIES AT LARGE",
					color: Color.Red * (0.5f + ( Main.rand.NextFloat() * 0.5f) ),
					priority: gauges.RedRealPercent >= 0.99f ? 1f : float.Epsilon
				);
			} );

			PKEMeter.PKEMeterAPI.SetPKERedTooltip( () => "AMBIENT" );
		}
	}
}