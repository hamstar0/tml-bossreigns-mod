using Terraria.ModLoader;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		private static bool? _OldEnableOrbUseUponTiles = null;
		private static string _OldOrbDisabledMessage = null;



		////////////////

		public static void ApplyOrbsBossReignEffects() {
			var config = BossReignsConfig.Instance;
			if( !config.Get<bool>( nameof(config.BlockOrbUseDuringReign) ) ) {
				BossReignsMod.UnapplyOrbsBossReignEffects();

				return;
			}

			var orbConfig = Orbs.OrbsConfig.Instance;
			
			if( !BossReignsMod._OldEnableOrbUseUponTiles.HasValue ) {
				BossReignsMod._OldEnableOrbUseUponTiles = orbConfig.Get<bool>( nameof(orbConfig.EnableOrbUseUponTiles) );
				BossReignsMod._OldOrbDisabledMessage = orbConfig.Get<string>( nameof(orbConfig.DisabledOrbMessage) );
			}

			orbConfig.SetOverride<bool>( nameof(orbConfig.EnableOrbUseUponTiles), false );
			orbConfig.SetOverride<string>( nameof(orbConfig.DisabledOrbMessage), "PKE interference disrupts orb use." );
		}

		public static void UnapplyOrbsBossReignEffects() {
			var orbConfig = Orbs.OrbsConfig.Instance;

			if( BossReignsMod._OldEnableOrbUseUponTiles.HasValue ) {
				orbConfig.SetOverride<bool>(
					nameof(orbConfig.EnableOrbUseUponTiles ),
					BossReignsMod._OldEnableOrbUseUponTiles.Value
				);
				BossReignsMod._OldEnableOrbUseUponTiles = null;
			}

			if( BossReignsMod._OldOrbDisabledMessage != null ) {
				orbConfig.SetOverride<string>(
					nameof(orbConfig.DisabledOrbMessage),
					BossReignsMod._OldOrbDisabledMessage
				);
				BossReignsMod._OldOrbDisabledMessage = null;
			}
		}
	}
}