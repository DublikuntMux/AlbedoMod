using Albedo.Base;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Combined
{
    public class PrehardmodBulletProjectile : BasBulletProjectile
    {
        protected override string Name => "PreHardmod Bullet";
        protected override int Penetrate => 10;
        
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
            target.AddBuff(BuffID.Slow, 600, false);
            target.AddBuff(BuffID.Chilled, 600, false);
            target.AddBuff(BuffID.Frostburn, 600, false);
        }
    }
}