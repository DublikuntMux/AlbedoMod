using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Projectiles.Boss.HellGuard
{
  public class CrystalBomb : ModProjectile
  {
    public override void SetStaticDefaults() => DisplayName.SetDefault("Crystal Bomb");

    public override void SetDefaults()
    {
      projectile.width = 30;
      projectile.height = 30;
      projectile.aiStyle = -1;
      projectile.hostile = true;
      projectile.timeLeft = 600;
      projectile.alpha = byte.MaxValue;
      projectile.hide = true;
      cooldownSlot = 1;
      projectile.scale = 2.5f;
      projectile.tileCollide = false;
    }

    public override void AI()
    {
      if (projectile.localAI[0] == 0.0)
      {
        projectile.localAI[0] = Main.rand.NextBool(2)? 1f : -1f;
        projectile.rotation = Main.rand.NextFloat(6.28f);
        projectile.hide = false;
      }
      if (--projectile.localAI[1] < 0.0)
      {
        projectile.localAI[1] = 60f;
        Main.PlaySound(SoundID.Item27, projectile.position);
      }
      projectile.alpha -= 10;
      if (projectile.alpha < 0)
        projectile.alpha = 0;
      if (projectile.alpha > byte.MaxValue)
        projectile.alpha = byte.MaxValue;
      projectile.rotation += (float) Math.PI / 40f * projectile.localAI[0];
      Lighting.AddLight(projectile.Center, 0.3f, 0.75f, 0.9f);
      int index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 197, 0.0f, 0.0f, 100, Color.Transparent);
      Main.dust[index].noGravity = true;
      Projectile projectile1 = projectile;
      projectile1.velocity *= 1.03f;
      if (projectile1.Center.Y <= (double) projectile1.ai[0])
        return;
      projectile1.tileCollide = true;
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      target.AddBuff(30, 180, true);
      target.AddBuff(33, 180, true);
    }

    public override void Kill(int timeLeft)
    {
      Main.PlaySound(SoundID.Item27, projectile.position);
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity *= 1.5f;
        Main.dust[index2].scale *= 0.9f;
      }
      if (Main.netMode == NetmodeID.MultiplayerClient)
        return;
      for (int index = 0; index < 24; ++index)
      {
        float num2 = projectile.velocity.Length() * Main.rand.Next(-60, 61) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
        float num3 = projectile.velocity.Length() * Main.rand.Next(-60, 61) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, num2, num3, ModContent.ProjectileType<CrystalBombShard>(), projectile.damage, 0f, projectile.owner);

      }
    }

    public override Color? GetAlpha(Color lightColor) => new Color?(Color.White * projectile.Opacity);

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D texture2D = Main.projectileTexture[projectile.type];
      int num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
      int num2 = num1 * projectile.frame;
      Rectangle rectangle = new Rectangle(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Utils.Size(rectangle) / 2f;
      projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
