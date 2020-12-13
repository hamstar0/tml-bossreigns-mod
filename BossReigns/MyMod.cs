using Terraria.ModLoader;


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

			if( config.DebugModeFastTime ) {
				maxTicks /= 60;
			}

			if( ModLoader.GetMod("Orbs") != null ) {
				if( myworld.ElapsedPresenceTicks >= maxTicks ) {
					BossReignsMod.ApplyOrbsBossReignEffects();
				} else {
					BossReignsMod.UnapplyOrbsBossReignEffects();
				}
			}
		}
	}
}