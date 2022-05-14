using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class SickleSplit1 : Sickle
    {
        public override string Texture => "Albedo/Projectiles/Boss/GunGod/Sickle";

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.timeLeft = 90;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }

            var projectile1 = projectile;
            projectile1.rotation += 0.8f;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                for (var i = 0; i < 8; i++)
                {
                    var vector = Vector2.Normalize(projectile.velocity).RotatedBy(Math.PI / 4.0 * i);
                    Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<SickleSplit2>(),
                        projectile.damage, 0f, projectile.owner);
                }
        }
    }
}