using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ModLoader;
using Terraria.ID;

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
			return AlbedoUtils.CustomRarity(3027, line);
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
