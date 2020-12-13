using Terraria;
using Terraria.ModLoader;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		public static void UpdateReignCursedBrambles( Player player, bool isReign ) {
			if( isReign ) {
				CursedBrambles.CursedBramblesAPI.SetPlayerToCreateBrambleWake( player, 64, 15 );
			} else {
				CursedBrambles.CursedBramblesAPI.UnsetPlayerToCreateBrambleWake( player );
			}
		}
	}
}