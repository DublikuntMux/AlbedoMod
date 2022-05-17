using Albedo.Base;
using Albedo.Global;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.CraftStations
{
    public class WeaponStation3 : AlbedoItem
    {
        protected override int Rarity => 6;

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

        public override void AddRecipes()
        {
            var val = new ModRecipe(mod);
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