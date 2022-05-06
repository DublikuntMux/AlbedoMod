using Albedo.Projectiles.Combined;
using Terraria.ModLoader;
using Terraria.ID;

namespace Albedo.Items.Ammos.Combined
{
    public class CosmoPouch : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 26;
            item.height = 26;
            item.knockBack = 8f;
            item.consumable = false;
            item.maxStack = 1;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<CosmoBulletProjectile>();
            item.shootSpeed = 15f;
            item.ammo = AmmoID.Bullet;
        }
        
        public override void AddRecipes()
        {
            ModRecipe val = new ModRecipe(mod);
            val.AddIngredient(ModContent.ItemType<PrehardmodePouch>());
            val.AddIngredient(ModContent.ItemType<HardmodePouch>());
            val.AddIngredient(ModContent.ItemType<PostLunarPouch>());
            val.AddTile(TileID.LunarCraftingStation);
            val.SetResult(this, 1);
            val.AddRecipe();
        }
    }
}
