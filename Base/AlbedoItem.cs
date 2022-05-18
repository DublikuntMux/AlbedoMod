using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class AlbedoItem : ModItem
	{
		protected abstract int Rarity { get; }

		public override void SetDefaults()
		{
			switch (Rarity) {
				case 12:
				case 11:
					item.rare = ItemRarityID.Expert;
					break;
				case 10:
				case 9:
					item.rare = ItemRarityID.Red;
					break;
				case 8:
				case 7:
				case 6:
					item.rare = ItemRarityID.Yellow;
					break;
				case 5:
					item.rare = ItemRarityID.Orange;
					break;
				case 4:
					item.rare = ItemRarityID.Green;
					break;
				case 3:
					item.rare = ItemRarityID.Blue;
					break;
				case 2:
					item.rare = ItemRarityID.White;
					break;
				case 1:
					item.rare = ItemRarityID.Gray;
					break;
			}
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			int rarity = 0;
			switch (Rarity) {
				case 12:
					rarity = 3556; //RainbowMid
					break;
				case 11:
					rarity = 2870; //Rainbow
					break;
				case 10:
					rarity = 3039; //Cosmos
					break;
				case 9:
					rarity = 3025; //Purple
					break;
				case 8:
					rarity = 3027; //Gold
					break;
				case 7:
					rarity = 3553; //Copper
					break;
				case 6:
					rarity = 1053; //Yellow
					break;
				case 5:
					rarity = 3550; //Orange
					break;
				case 4:
					rarity = 3551; //Green
					break;
				case 3:
					rarity = 3552; //Blue
					break;
				case 2:
					rarity = 3026; //White
					break;
				case 1:
					rarity = 3026; //Black
					break;
			}

			if (line.mod == "Terraria" && line.Name == "ItemName") {
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(rarity), item);
				Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
				return false;
			}

			return true;
		}
	}
}