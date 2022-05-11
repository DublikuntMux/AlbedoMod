using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Combined
{
    public class PrehardmodBulletProjectile : ModProjectile
    {
        private int _bounce = 25;
        private readonly int[] _dusts = {133, 134};
        private int _currentDust;
        
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
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (_bounce > 1)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                _bounce--;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = 0f - oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = 0f - oldVelocity.Y;
                }
            }
            else
            {
                projectile.Kill();
            }
            return false;
        }
        
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * i;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * i;
                int num = Dust.NewDust(new Vector2(x, y), 1, 1, _dusts[_currentDust], 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].alpha = projectile.alpha;
                Main.dust[num].position.X = x;
                Main.dust[num].position.Y = y;
                Dust obj = Main.dust[num];
                obj.velocity *= 0f;
                Main.dust[num].noGravity = true;
            }
            _currentDust++;
            if (_currentDust > 1)
            {
                _currentDust = 0;
            }
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
