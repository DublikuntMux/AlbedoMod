using Albedo.Items.Ammos.Pouches.Mod;
using Albedo.Items.Ammos.Pouches.Vanila;
using Albedo.Projectiles.Combined;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Combined
{
    public class PrehardmodePouch : ModItem
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
            item.shoot = ModContent.ProjectileType<PrehardmodBulletProjectile>();
            item.shootSpeed = 15f;
            item.ammo = AmmoID.Bullet;
        }
        
        public override void AddRecipes()
        {
            ModRecipe val = new ModRecipe(mod);
            val.AddIngredient(ModContent.ItemType<ExplosivePouch>());
            val.AddIngredient(ModContent.ItemType<GoldenPouch>());
            val.AddIngredient(ModContent.ItemType<MeteorPouch>());
            val.AddIngredient(ModContent.ItemType<SilverPouch>());
            val.AddIngredient(ModContent.ItemType<CopperPouch>());
            val.AddIngredient(ModContent.ItemType<DirtPouch>());
            val.AddIngredient(ModContent.ItemType<GelPouch>());
            val.AddIngredient(ModContent.ItemType<GemPouch>());
            val.AddIngredient(ModContent.ItemType<IcePouch>());
            val.AddIngredient(ModContent.ItemType<IronPouch>());
            val.AddIngredient(ModContent.ItemType<LeadPouch>());
            val.AddIngredient(ModContent.ItemType<SnowPouch>());
            val.AddIngredient(ModContent.ItemType<StonePouch>());
            val.AddIngredient(ModContent.ItemType<TungstenPouch>());
            val.AddTile(TileID.CrystalBall);
            val.SetResult(this, 1);
            val.AddRecipe();
        }
    }
}
