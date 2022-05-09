using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ModLoader;
using Terraria.ID;

namespace Albedo.Items.CraftStations
{
	public class WeaponStation1 : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 14;
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<WeaponStation1Tile>();
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.CustomRarity(3027, line);
		}
		
		public override void AddRecipes()
		{
			ModRecipe val = new ModRecipe(mod);
			val.AddIngredient(ItemID.Furnace);
			val.AddIngredient(ItemID.Star, 5);
			val.AddIngredient(ItemID.Ruby, 5);
			val.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 5);
			val.AddTile(TileID.Anvils);
			val.SetResult(this);
			val.AddRecipe();
		}
	}
}
