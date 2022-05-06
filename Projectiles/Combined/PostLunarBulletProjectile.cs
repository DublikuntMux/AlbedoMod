using Albedo.Base;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Combined
{
    public class PostLunarBulletProjectile : BasBulletProjectile
    {
        protected override string Name => "PostLunar Bullet";
        protected override int Penetrate => -1;
        
        public override void SetDefaults()
        {
            base.SetStaticDefaults();
            projectile.timeLeft = 200;
            projectile.alpha = 255;
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            target.AddBuff(BuffID.Electrified, 600, false);
        }
    }
}