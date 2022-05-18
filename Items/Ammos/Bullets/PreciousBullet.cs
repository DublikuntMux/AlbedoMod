using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class PreciousBullet : BaseBullet
	{
		protected override float ShootSpeed => 0.5f;
		protected override int Damage => 12;
		protected override float KnockBack => 0.25f;
		protected override int Price => Item.buyPrice(copper: 5);
		protected override int BulletMaterial => ItemID.GoldBar;
		protected override int BulletProjectile => ModContent.ProjectileType<PreciousBulletProjectile>();

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 25);
			recipe.AddRecipe();
		}
	}
}