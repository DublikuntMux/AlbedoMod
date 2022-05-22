using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class CobaltBullet : BaseBullet
	{
		protected override float ShootSpeed => 4f;
		protected override int Damage => 9;
		protected override float KnockBack => 1.50f;
		protected override int Price => Item.buyPrice(copper: 30);
		protected override int BulletMaterial => ItemID.CobaltBar;
		protected override int BulletProjectile => ModContent.ProjectileType<CobaltBulletProjectile>();
	}
}