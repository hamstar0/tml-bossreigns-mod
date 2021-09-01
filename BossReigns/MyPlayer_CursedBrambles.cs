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
				CursedBrambles.CursedBramblesAPI.SetPlayerToCreateBrambleWake( player, true, 64, 15 );
			} else {
				CursedBrambles.CursedBramblesAPI.UnsetPlayerBrambleWakeCreating( player );
			}
		}
	}
}