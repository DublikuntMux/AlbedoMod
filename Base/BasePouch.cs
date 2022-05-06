using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using static Terraria.Main;

namespace Albedo.Base
{
	public abstract class BasePouch : ModItem
	{
		protected abstract int AmmunitionItem { get; }

		public override void SetDefaults()
		{
			item.CloneDefaults(AmmunitionItem);
			item.width = 26;
			item.height = 26;
			item.consumable = false;
			item.maxStack = 1;
			Item item1 = item;
			item1.value *= 3996;
			item.rare = ItemRarityID.Yellow;
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3027), item, (DrawData?)null);
				Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
				return false;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe val = new ModRecipe(mod);
			val.AddIngredient(AmmunitionItem, 3996);
			val.AddTile(TileID.CrystalBall);
			val.SetResult(this, 1);
			val.AddRecipe();
		}
	}
}
