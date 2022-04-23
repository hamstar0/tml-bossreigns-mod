using Terraria.ModLoader;


namespace BossReigns {
	public partial class BossReignsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-bossreigns-mod";


		////////////////

		public static BossReignsMod Instance { get; private set; }



		////////////////
		
		public Mod NecrotisMod { get; private set; }



		////////////////

		public override void Load() {
			BossReignsMod.Instance = this;

			this.NecrotisMod = ModLoader.GetMod( "Necrotis" );
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
			if( ModLoader.GetMod("Orbs") != null ) {
				if( BossReignsAPI.GetReignBuildup(out int maxTicks) >= maxTicks ) {
					BossReignsMod.ApplyOrbsBossReignEffects();
				} else {
					BossReignsMod.UnapplyOrbsBossReignEffects();
				}
			}
		}
	}
	class BossReignsTile : GlobalTile {
		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			if( ModLoader.GetMod( "Orbs" ) != null ) {
				return;
			}

			//

			var config = BossReignsConfig.Instance;

			if( !config.Get<bool>( nameof(config.BlockMiningDuringReignIfNoOrbs) ) ) {
				return;
			}

			//

			if( BossReignsAPI.GetReignBuildup(out int maxTicks) >= maxTicks ) {
				fail = true;
				effectOnly = true;
				noItem = true;
			}
		}
	}
}