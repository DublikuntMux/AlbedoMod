using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
    public class IceBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 6;
            item.ranged = true;
            item.width = 40;
            item.height = 40;
            item.knockBack = 0.50f;
            item.value = Item.buyPrice(copper: 1);
            item.rare = ItemRarityID.Blue;
            item.consumable = true;
            item.shoot = ModContent.ProjectileType<IceBulletProjectile>();
            item.ammo = AmmoID.Bullet;
            item.maxStack = 999;
            item.shootSpeed = 0.5f;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }
    }
}