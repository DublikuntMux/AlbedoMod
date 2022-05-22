using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class PinkGelBullet : BaseBullet
	{
		protected override float ShootSpeed => 6f;
		protected override int Damage => 5;
		protected override float KnockBack => 4f;
		protected override int Price => Item.buyPrice(copper: 20);
		protected override int BulletMaterial => ItemID.PinkGel;
		protected override int BulletProjectile => ModContent.ProjectileType<PinkGelBulletProjectile>();
	}
}