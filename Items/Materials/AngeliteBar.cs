using Albedo.Base;
using Albedo.Tiles.Bars;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Materials
{
    public class AngeliteBar : AlbedoItem
    {
        protected override int Rarity => 4;

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.value = 17500;
            item.createTile = ModContent.TileType<AngeliteBarTile>();
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AngeliteBar>(), 6);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}