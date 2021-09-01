using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private static void UpdateReignForNecrotis_WeakRef( Player player ) {
			var config = BossReignsConfig.Instance;
			float drainPer10Min = config.Get<float>( nameof(config.ReignNecrotisDrainPerPer10Min) );
			float drainPerTick = drainPer10Min / 10f;	// 10 min
			drainPerTick /= 60f;	// 1 min
			drainPerTick /= 60f;	// 1 tick

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer(
				player: player,
				percent: Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer(player) - drainPerTick,
				quiet: true,
				sync: false
			);
		}
	}
}