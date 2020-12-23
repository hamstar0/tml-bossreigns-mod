using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		private static float LastGaugedBackgroundPKEPercent = 0f;



		////////////////

		public static void InitializePKE() {
			PKEMeter.Logic.PKEGauge gauge = PKEMeter.PKEMeterAPI.GetGauge();
			PKEMeter.Logic.PKEText meterTextFunc = PKEMeter.PKEMeterAPI.GetMeterText();

			int gaugeTimer = 0;
			int textTimer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				(float b, float g, float y, float r) existingGauge = gauge?.Invoke( plr, pos )
					?? (0f, 0f, 0f, 0f);

				if( gaugeTimer-- <= 0 ) {
					gaugeTimer = 10;
					BossReignsMod.LastGaugedBackgroundPKEPercent = BossReignsMod.GaugeBackgroundPKE( pos ) ?? 0f;
				}

				existingGauge.r = BossReignsMod.LastGaugedBackgroundPKEPercent;   // Red channel
				return existingGauge;
			} );

			PKEMeter.PKEMeterAPI.SetMeterText( ( plr, pos, gauges ) => {
				(string text, Color color) currText = meterTextFunc?.Invoke( plr, pos, gauges )
					?? ("", Color.Transparent);
				if( textTimer <= 0 && currText.text != "" ) {   // yield
					return currText;
				}

				if( gauges.r > 0.75f || textTimer > 0 ) {
					if( gauges.r > 0.75f ) {
						textTimer = 60;
					}

					currText.color = Color.Red * ( 0.5f + ( Main.rand.NextFloat() * 0.5f ) );
					currText.text = "WARNING - CLASS V+ PKE-EMITTING ENTITIES AT LARGE";
				}

				textTimer--;

				return currText;
			} );
		}

		////

		public static float? GaugeBackgroundPKE( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			double elapsedTicks = (double)myworld.ElapsedPresenceTicks;

			var config = BossReignsConfig.Instance;
			double maxTicks = (double)config.Get<int>( nameof(config.TicksUntilReign) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60d;
			}

			return MathHelper.Clamp( (float)(elapsedTicks / maxTicks), 0f, 1f );
		}
	}
}