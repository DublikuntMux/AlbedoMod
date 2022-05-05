using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class IronBullet : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 9;
			item.ranged = true;
			item.width = 40;
			item.height = 40;
			item.knockBack = 0.25f;
			item.value = Item.buyPrice(copper:1);
			item.rare = ItemRarityID.White;
			item.consumable = true;
			item.shoot = ModContent.ProjectileType<IronBulletProjectile>();
			item.ammo = AmmoID.Bullet;
			item.maxStack = 999;
			item.shootSpeed = 0.5f;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 2);
			recipe.anyIronBar = true;
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, stack:25);
			recipe.AddRecipe();
		}
	}
}
