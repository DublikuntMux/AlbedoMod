using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class MythrilBullet : BaseBullet
	{
		protected override float ShootSpeed => 5f;
		protected override int Damage => 9;
		protected override float KnockBack => 2f;
		protected override int Price => Item.buyPrice(copper: 40);
		protected override int BulletMaterial => ItemID.MythrilBar;
		protected override int BulletProjectile => ModContent.ProjectileType<MythrilBulletProjectile>();
	}
}