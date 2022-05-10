using System;
using Albedo.Global;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.HellGuard
{
  public class MoonLordSunBlast : EarthChainBlast
  {
    public override string Texture => "Terraria/Projectile_687";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      DisplayName.SetDefault("Sun Blast");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      projectile.width = 70;
      projectile.height = 70;
      cooldownSlot = 1;
      projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 1;
    }

    public override bool CanDamage() => projectile.frame == 3 || projectile.frame == 4;

    public override void AI()
    {
      if (projectile.position.HasNaNs())
      {
        projectile.Kill();
      }
      else
      {
        if (++projectile.frameCounter >= 2)
        {
          projectile.frameCounter = 0;
          if (++projectile.frame >= Main.projFrames[projectile.type])
          {
            --projectile.frame;
            projectile.Kill();
            return;
          }
        }
        if (projectile.localAI[1] == 0.0)
        {
          Main.PlaySound(SoundID.Item88, projectile.Center);
          projectile.position = projectile.Center;
          projectile.scale = Main.rand.NextFloat(1.5f, 4f);
          projectile.rotation = Main.rand.NextFloat(6.283185f);
          projectile.width = (int) (projectile.width * (double) projectile.scale);
          projectile.height = (int) (projectile.height * (double) projectile.scale);
          projectile.Center = projectile.position;
        }
        if (++projectile.localAI[1] != 6.0 || projectile.ai[1] <= 0.0 || Main.netMode == NetmodeID.MultiplayerClient)
          return;
        --projectile.ai[1];
        Vector2 vector = projectile.ai[0].ToRotationVector2();
        float num3 = MathHelper.ToRadians(15f);
        if (projectile.localAI[0] != 2.0)
        {
          float num = Math.Min(5f, projectile.ai[1]);
          int index = Projectile.NewProjectile(projectile.Center + Main.rand.NextVector2Circular(20f, 20f), Vector2.Zero, projectile.type, projectile.damage, 0f, projectile.owner, projectile.ai[0], num);
          if (index != 1000)
            Main.projectile[index].localAI[0] = 1f;
        }
        if (projectile.localAI[0] == 1.0)
          return;
        Vector2 vector2 = projectile.width / projectile.scale * 10f / 7f * Utils.RotatedBy(vector, (double)Main.rand.NextFloat(0f - num3, num3), default(Vector2));
        int index1 = Projectile.NewProjectile(projectile.Center + vector2, Vector2.Zero, projectile.type, projectile.damage, 0f, projectile.owner, projectile.ai[0], projectile.ai[1]);
        if (index1 == 1000)
          return;
        Main.projectile[index1].localAI[0] = projectile.localAI[0];
      }
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      target.AddBuff(67, 120, true);
      target.AddBuff(24, 300, true);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D texture2D = Main.projectileTexture[projectile.type];
      int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
      int y = num * projectile.frame;
      Rectangle rectangle = new Rectangle(0, y, texture2D.Width, num);
      Vector2 origin = Utils.Size(rectangle) / 2f;
      Color color = new Color(255, 255, 255, 200);
      Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
      return false;
    }
  }
}
