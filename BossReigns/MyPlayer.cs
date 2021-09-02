using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace BossReigns {
	partial class BossReignsPlayer : ModPlayer {
		private bool IsReignSinceLastTick = false;

		private int ReignTimer = 0;

		private int ShakeIntermissionTimer = 0;



		////////////////

		public override void PreUpdate() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();
			bool isReign = myworld.IsReign();

			if( this.ReignTimer-- == 0 ) {
				this.ReignTimer = 5;
				this.UpdateReignPer5Ticks( isReign );
			}

			this.UpdateReignPerTick( isReign );

			this.IsReignSinceLastTick = isReign;
		}
	}
}