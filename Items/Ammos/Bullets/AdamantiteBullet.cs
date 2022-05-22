using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class AdamantiteBullet : BaseBullet
	{
		protected override float ShootSpeed => 6f;
		protected override int Damage => 9;
		protected override float KnockBack => 3f;
		protected override int Price => Item.buyPrice(copper: 50);
		protected override int BulletMaterial => ItemID.AdamantiteBar;
		protected override int BulletProjectile => ModContent.ProjectileType<AdamantiteBulletProjectile>();
	}
}