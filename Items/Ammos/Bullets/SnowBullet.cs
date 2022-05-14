using Albedo.Base;
using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
    public class SnowBullet : BaseBullet
    {
        protected override float ShootSpeed => 1f;
        protected override int Damage => 5;
        protected override float KnockBack => 0.25f;
        protected override int Price => Item.buyPrice(copper: 1);
        protected override int BulletMaterial => ItemID.SnowBlock;
        protected override int BulletProjectile => ModContent.ProjectileType<SnowBulletProjectile>();
    }
}