using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class GelBullet : BaseBullet
	{
		protected override float ShootSpeed => 4.5f;
		protected override int Damage => 6;
		protected override float KnockBack => 2f;
		protected override int Price => Item.buyPrice(copper: 2);
		protected override int BulletMaterial => ItemID.Gel;
		protected override int BulletProjectile => ModContent.ProjectileType<GelBulletProjectile>();
	}
}