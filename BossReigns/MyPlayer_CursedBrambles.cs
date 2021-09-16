using Terraria;
using Terraria.ModLoader;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		public static void UpdateReignForCursedBrambles_WeakRef( Player player, bool isReign ) {
			var config = BossReignsConfig.Instance;
			if( !config.Get<bool>( nameof(config.CreateUndergroundCursedBramblesDuringReign) ) ) {
				return;
			}

			if( isReign ) {
				CursedBrambles.CursedBramblesAPI.SetPlayerToCreateBrambleWake(
					player: player,
					isElevationChecked: true,
					radius: 64,
					tickRate: 15,
					validateAt: CursedBrambles.CursedBramblesAPI.CreatePlayerAvoidingBrambleValidator( 12 )
				);
			} else {
				CursedBrambles.CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
			}
		}
	}
}