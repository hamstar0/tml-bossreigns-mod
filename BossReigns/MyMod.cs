using Terraria.ModLoader;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-bossreigns-mod";


		////////////////

		public static BossReignsMod Instance { get; private set; }



		////////////////

		public override void Load() {
			BossReignsMod.Instance = this;
		}

		public override void PostSetupContent() {
			if( ModLoader.GetMod("PKEMeter") != null ) {
				BossReignsMod.InitializePKE();
			}
		}

		public override void Unload() {
			BossReignsMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();

			var config = BossReignsConfig.Instance;
			int maxTicks = config.Get<int>( nameof(config.TicksUntilReign) );

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
			}

			if( ModLoader.GetMod("Orbs") != null ) {
				if( myworld.ElapsedReignBuildupTicks >= maxTicks ) {
					BossReignsMod.ApplyOrbsBossReignEffects();
				} else {
					BossReignsMod.UnapplyOrbsBossReignEffects();
				}
			}
		}
	}
}