using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using static Terraria.Main;

namespace Albedo.Items.CraftStations
{
	public class WeaponStation3 : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 14;
			item.rare = ItemRarityID.Red;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<WeaponStation3Tile>();
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
			val.AddIngredient(ModContent.ItemType<WeaponStation2>());
			val.AddIngredient(ItemID.LunarBar, 15);
			val.AddIngredient(ItemID.Ectoplasm, 20);
			val.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 50);
			val.AddTile(TileID.LunarCraftingStation);
			val.SetResult(this);
			val.AddRecipe();
		}
	}
}
