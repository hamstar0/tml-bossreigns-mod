using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private static void UpdateReignForNecrotis_WeakRef( Player player ) {
			var config = BossReignsConfig.Instance;
			float drainPer10Min = config.Get<float>( nameof(config.ReignNecrotisAnimaDrainPerPer10Min) );
			float drainPerTick = drainPer10Min / (10f * 60f * 60f);

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer(
				player: player,
				percent: drainPerTick,
				quiet: true,
				sync: false
			);
		}
	}
}