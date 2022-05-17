using Albedo.Base;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.CraftStations
{
    public class WeaponStation2 : AlbedoItem
    {
        protected override int Rarity => 5;

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
            item.createTile = ModContent.TileType<WeaponStation2Tile>();
        }

        public override void AddRecipes()
        {
            var val = new ModRecipe(mod);
            val.AddIngredient(ModContent.ItemType<WeaponStation1>());
            val.AddIngredient(ItemID.MythrilAnvil);
            val.AddIngredient(ItemID.Sapphire, 15);
            val.AddIngredient(ItemID.Amethyst, 15);
            val.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 25);
            val.AddTile(TileID.MythrilAnvil);
            val.SetResult(this);
            val.AddRecipe();
        }
    }
}