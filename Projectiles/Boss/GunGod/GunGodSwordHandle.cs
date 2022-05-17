using System;
using System.Linq;
using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.GunGod
{
    public class GunGodSwordHandle : BaseDeathray
    {
        public int Counter;

        public GunGodSwordHandle()
            : base(150f, "GunGodDeathray")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
        }

        public override void AI()
        {
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
                projectile.velocity = -Vector2.UnitY;
            var byUuidReal = BossHelper.GetByUuidReal(projectile.owner, (int) projectile.ai[1],
                ModContent.ProjectileType<GunGodSword>());
            if (byUuidReal != -1)
            {
                projectile.Center = Main.projectile[byUuidReal].Center + Main.projectile[byUuidReal].velocity * 75f;
                projectile.velocity = Main.projectile[byUuidReal].velocity.RotatedBy(projectile.ai[0]);
            }

            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
                projectile.velocity = -Vector2.UnitY;
            _ = projectile.localAI[0];
            _ = 0f;
            var num = 1f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= maxTime)
            {
                projectile.Kill();
                return;
            }

            projectile.scale = (float) Math.Sin(projectile.localAI[0] * (float) Math.PI / maxTime) * num * 6f;
            if (projectile.scale > num) projectile.scale = num;
            var num2 = projectile.velocity.ToRotation();
            projectile.rotation = num2 - (float) Math.PI / 2f;
            projectile.velocity = num2.ToRotationVector2();
            var num3 = 3f;
            _ = projectile.width;
            var array = new float[(int) num3];
            for (var i = 0; i < array.Length; i++) array[i] = 100f;
            var num4 = array.Sum();
            num4 /= num3;
            var amount = 0.5f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num4, amount);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.velocity.X = target.Center.X < Main.projectile[(int) projectile.ai[1]].Center.X ? -15f : 15f;
            target.velocity.Y = -10f;
            target.AddBuff(195, 600);
            target.AddBuff(196, 600);
        }
    }
}