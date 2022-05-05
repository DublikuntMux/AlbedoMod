using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class CopperBullet : BaseBullet
	{
		protected override float ShootSpeed => 1f;
		protected override int Damage => 7;
		protected override float KnockBack => 0.50f;
		protected override int Price => Item.buyPrice(copper:1);
		protected override int BulletMaterial => ItemID.CopperBar;
		protected override int BulletProjectile => ModContent.ProjectileType<CopperBulletProjectile>();
	}
}
