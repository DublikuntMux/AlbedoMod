using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class Flocko2 : Flocko
    {
        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.ai[0] < 0f || projectile.ai[0] >= 255f)
            {
                projectile.Kill();
                return;
            }

            var val = Main.player[(int) projectile.ai[0]];
            var center = val.Center;
            center.X += 700f * projectile.ai[1];
            var vector = center - projectile.Center;
            if (vector.Length() > 100f)
            {
                vector /= 8f;
                projectile.velocity = (projectile.velocity * 23f + vector) / 24f;
            }
            else if (projectile.velocity.Length() < 12f)
            {
                var projectile1 = projectile;
                projectile1.velocity *= 1.05f;
            }

            if ((projectile.localAI[0] += 1f) > 90f && (projectile.localAI[1] += 1f) > 60f)
            {
                projectile.localAI[1] = 0f;
                Main.PlaySound(SoundID.Item120, projectile.position);
            }

            var projectile3 = projectile;
            if (++projectile3.frameCounter > 3)
            {
                var projectile4 = projectile;
                if (++projectile4.frame >= 6) projectile.frame = 0;
                projectile.frameCounter = 0;
            }
        }
    }
}