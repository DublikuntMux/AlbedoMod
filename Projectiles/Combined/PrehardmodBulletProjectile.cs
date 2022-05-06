using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Combined
{
    public class PrehardmodBulletProjectile : ModProjectile
    {

        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("PreHardmod Bullet");
        }

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 4;
            projectile.height = 20;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 20;
            projectile.timeLeft = 400;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.scale = 0.7f;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 200;
            projectile.alpha = 255;
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        
        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.1f, 0.1f, 0.9f);
            Lighting.Brightness(1, 1);
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
