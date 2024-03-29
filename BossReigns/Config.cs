﻿using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace BossReigns {
	class MyFloatInputElement : FloatInputElement { }




	public partial class BossReignsConfig : ModConfig {
		public static BossReignsConfig Instance => ModContent.GetInstance<BossReignsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeFastTime { get; set; } = false;

		////

		[DefaultValue( true )]
		public bool BlockMiningDuringReignIfNoOrbs { get; set; } = true;

		[DefaultValue( true )]
		public bool BlockOrbUseDuringReign { get; set; } = true;
		
		public bool CreateUndergroundCursedBramblesDuringReign { get; set; } = false;
		
		////
		
		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 5 )]
		public int AddedTicksUntilFirstReign { get; set; } = 60 * 60 * 24 * 5;	// 5 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 3 )]
		public int TicksUntilReign { get; set; } = 60 * 60 * 24 * 3;	// 3 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * -3 )]
		public int MinimumTicksElapsed { get; set; } = 60 * 60 * 24 * -3;	// -3 days

		[Range( 60 * 60, 60 * 60 * 24 * 100 )]
		[DefaultValue( 60 * 60 * 24 * 4 )]
		public int TicksRemovedFromEachBossKill { get; set; } = 60 * 60 * 24 * 4;   // 4 days

		////

		[Range( -100f, 100f )]
		[DefaultValue( 0.25f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float ReignNecrotisAnimaDrainPerPer10Min { get; set; } = 0.25f;	// 4 days
	}
}
