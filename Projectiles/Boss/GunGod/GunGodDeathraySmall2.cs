using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class GunGodDeathraySmall2 : BaseDeathray
    {
        public GunGodDeathraySmall2()
            : base(30f, "GunGod Deathray")
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GunGod Deathray");
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
                projectile.velocity = -Vector2.UnitY;
            var val = AlbedoUtils.NpcExists(projectile.ai[1], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
            if (val == null)
            {
                projectile.Kill();
                return;
            }

            projectile.Center = val.Center;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
                projectile.velocity = -Vector2.UnitY;
            var num = 0.3f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= maxTime)
            {
                projectile.Kill();
                return;
            }

            projectile.scale = (float) Math.Sin(projectile.localAI[0] * (float) Math.PI / maxTime) * 0.6f * num;
            if (projectile.scale > num) projectile.scale = num;
            var num2 = 3f;
            _ = projectile.width;
            var array = new float[(int) num2];
            for (var i = 0; i < array.Length; i++) array[i] = 3000f;
            var num3 = array.Sum();
            num3 /= num2;
            var amount = 0.5f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num3, amount);
            var vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (var k = 0; k < 2; k++)
            {
                var num4 = projectile.velocity.ToRotation() +
                           (Main.rand.NextBool(2) ? -1f : 1f) * ((float) Math.PI / 2f);
                var num5 = (float) Main.rand.NextDouble() * 2f + 2f;
                var vector3 = new Vector2((float) Math.Cos(num4) * num5, (float) Math.Sin(num4) * num5);
                var num6 = Dust.NewDust(vector2, 0, 0, 244, vector3.X, vector3.Y);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].scale = 1.7f;
            }

            if (Main.rand.NextBool(5))
            {
                var vector4 = projectile.velocity.RotatedBy(1.5707963705062866) *
                              ((float) Main.rand.NextDouble() - 0.5f) * projectile.width;
                var num7 = Dust.NewDust(vector2 + vector4 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default, 1.5f);
                var obj = Main.dust[num7];
                obj.velocity *= 0.5f;
                Main.dust[num7].velocity.Y = 0f - Math.Abs(Main.dust[num7].velocity.Y);
            }

            var projectile1 = projectile;
            projectile1.position -= projectile.velocity;
            projectile.rotation = projectile.velocity.ToRotation() - (float) Math.PI / 2f;
        }
    }
}