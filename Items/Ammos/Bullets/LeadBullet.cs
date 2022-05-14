using Albedo.Projectiles.Bullets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Bullets
{
    public class LeadBullet : DirtBullet
    {
        protected override float ShootSpeed => 1f;
        protected override int Damage => 9;
        protected override float KnockBack => 1f;
        protected override int Price => Item.buyPrice(copper: 1);
        protected override int BulletMaterial => ItemID.LeadBar;
        protected override int BulletProjectile => ModContent.ProjectileType<LeadBulletProjectile>();
    }
}