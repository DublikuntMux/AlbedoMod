using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
	public class StoneBullet : BaseBullet
	{
		protected override float ShootSpeed => 0.5f;
		protected override int Damage => 5;
		protected override float KnockBack => 1.5f;
		protected override int Price => Item.buyPrice(copper:1);
		protected override int BulletMaterial => ItemID.StoneBlock;
		protected override int BulletProjectile => ModContent.ProjectileType<StoneBulletProjectile>();
	}
}
