using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace BossReigns {
	class MyFloatInputElement : FloatInputElement { }




	public partial class BossReignsConfig : ModConfig {
		public static BossReignsConfig Instance { get; internal set; }



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeFastTime { get; set; } = false;

		////

		[DefaultValue( true )]
		public bool BlockOrbUseDuringReign { get; set; } = true;
		
		public bool CreateUndergroundCursedBramblesDuringReign { get; set; } = false;
		
		////
		
		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 4 )]
		public int AddedTicksUntilFirstReign { get; set; } = 60 * 60 * 24 * 4;	// 4 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 3 )]
		public int TicksUntilReign { get; set; } = 60 * 60 * 24 * 3;	// 3 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * -3 )]
		public int LowestAmountTicksBeforeReign { get; set; } = 60 * 60 * 24 * -3;	// -3 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 4 )]
		public int TicksRemovedFromEachBossKill { get; set; } = 60 * 60 * 24 * 4;	// 4 days
	}
}
