using Terraria.ModLoader;
using Orbs;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-bossreigns-mod";


		////////////////

		public static BossReignsMod Instance { get; private set; }



		////////////////

		public BossReignsMod() {
			BossReignsMod.Instance = this;
		}

		public override void Load() {
			BossReignsConfig.Instance = ModContent.GetInstance<BossReignsConfig>();
		}

		public override void Unload() {
			BossReignsConfig.Instance = null;
			BossReignsMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();

			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );

			if( myworld.ElapsedPresenceTicks >= maxTicks ) {
				this.ApplyReignEffects();
			} else {
				this.UnapplyReignEffects();
			}
		}


		////////////////

		private bool? _OldEnableOrbUseUponTiles = null;
		private string _OldOrbDisabledMessage = null;

		public void ApplyReignEffects() {
			var orbConfig = OrbsConfig.Instance;

			this._OldEnableOrbUseUponTiles = orbConfig.Get<bool>( nameof(orbConfig.EnableOrbUseUponTiles) );
			this._OldOrbDisabledMessage = orbConfig.Get<string>( nameof(orbConfig.OrbDisabledMessage) );

			orbConfig.SetOverride<bool>( "EnableOrbUseUponTiles", false );
			orbConfig.SetOverride<string>( "OrbDisabledMessage", "PKE interference disrupts orb use." );
		}

		public void UnapplyReignEffects() {
			var orbConfig = OrbsConfig.Instance;

			if( this._OldEnableOrbUseUponTiles.HasValue ) {
				orbConfig.SetOverride<bool>( "EnableOrbUseUponTiles", this._OldEnableOrbUseUponTiles.Value );
				this._OldEnableOrbUseUponTiles = null;
			} else {
				orbConfig.UnsetOverride<bool>( "EnableOrbUseUponTiles" );
			}

			if( this._OldOrbDisabledMessage != null ) {
				orbConfig.SetOverride<string>( "OrbDisabledMessage", this._OldOrbDisabledMessage );
				this._OldOrbDisabledMessage = null;
			} else {
				orbConfig.UnsetOverride<string>( "OrbDisabledMessage" );
			}
		}
	}
}