using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class IronBullet : BaseBullet
	{
		protected override float ShootSpeed => 0.5f;
		protected override int Damage => 9;
		protected override float KnockBack => 0.25f;
		protected override int Price => Item.buyPrice(copper: 1);
		protected override int BulletMaterial => ItemID.IronBar;
		protected override int BulletProjectile => ModContent.ProjectileType<IronBulletProjectile>();
	}
}