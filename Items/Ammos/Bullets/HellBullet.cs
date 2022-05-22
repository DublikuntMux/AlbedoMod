using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class HellBullet : BaseBullet
	{
		protected override float ShootSpeed => 3f;
		protected override int Damage => 8;
		protected override float KnockBack => 1.50f;
		protected override int Price => Item.buyPrice(copper: 20);
		protected override int BulletMaterial => ItemID.HellstoneBar;
		protected override int BulletProjectile => ModContent.ProjectileType<HellBulletProjectile>();
	}
}