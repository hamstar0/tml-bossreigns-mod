using Terraria.ModLoader;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		private static bool? _OldEnableOrbUseUponTiles = null;
		private static string _OldOrbDisabledMessage = null;



		////////////////

		public static void ApplyOrbsBossReignEffects() {
			var orbConfig = Orbs.OrbsConfig.Instance;

			BossReignsMod._OldEnableOrbUseUponTiles = orbConfig.Get<bool>( nameof(orbConfig.EnableOrbUseUponTiles) );
			BossReignsMod._OldOrbDisabledMessage = orbConfig.Get<string>( nameof(orbConfig.OrbDisabledMessage) );

			orbConfig.SetOverride<bool>( "EnableOrbUseUponTiles", false );
			orbConfig.SetOverride<string>( "OrbDisabledMessage", "PKE interference disrupts orb use." );
		}

		public static void UnapplyOrbsBossReignEffects() {
			var orbConfig = Orbs.OrbsConfig.Instance;

			if( BossReignsMod._OldEnableOrbUseUponTiles.HasValue ) {
				orbConfig.SetOverride<bool>( "EnableOrbUseUponTiles", BossReignsMod._OldEnableOrbUseUponTiles.Value );
				BossReignsMod._OldEnableOrbUseUponTiles = null;
			} else {
				orbConfig.UnsetOverride<bool>( "EnableOrbUseUponTiles" );
			}

			if( BossReignsMod._OldOrbDisabledMessage != null ) {
				orbConfig.SetOverride<string>( "OrbDisabledMessage", BossReignsMod._OldOrbDisabledMessage );
				BossReignsMod._OldOrbDisabledMessage = null;
			} else {
				orbConfig.UnsetOverride<string>( "OrbDisabledMessage" );
			}
		}
	}
}