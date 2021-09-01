using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace BossReigns {
	class BossReignsItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( !BossReignsWorld.IsViableSummonItem( item.type ) ) {
				return;
			}

			string modCtx = "[c/FFFF88:Boss Reigns] - ";
			var tip = new TooltipLine( this.mod, "BossReignsActive", modCtx + "This item pulses violently in anticipation..." );

			tooltips.Add( tip );
		}


		public override bool PreDrawInInventory( Item item, SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale ) {
			if( !BossReignsWorld.IsViableSummonItem( item.type ) ) {
				return true;
			}

			float shake = 0.25f;
			float newScale = scale * (1f - shake);
			newScale += Main.rand.NextFloat() * scale * (shake * 2f);

			var offset = new Vector2(frame.Width, frame.Height) * 0.5f;
			offset *= scale;

			sb.Draw(
				texture: Main.itemTexture[item.type],
				position: position + offset,
				sourceRectangle: frame,
				color: drawColor,
				rotation: 0f,
				origin: new Vector2(frame.Width, frame.Height) * 0.5f,
				scale: newScale,
				effects: SpriteEffects.None,
				layerDepth: 0
			);

			return false;
		}
	}
}
