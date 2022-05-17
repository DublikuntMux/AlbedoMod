using Albedo.Base;
using Albedo.Global;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.CraftStations
{
    public class WeaponStation1 : AlbedoItem
    {
        protected override int Rarity => 4;

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<WeaponStation1Tile>();
        }

        public override void AddRecipes()
        {
            var val = new ModRecipe(mod);
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