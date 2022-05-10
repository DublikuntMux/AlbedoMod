using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
  public class FlowerPetal : ModProjectile
  {
    public override string Texture => "Terraria/Projectile_221";

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Flower Petal");
      Main.projFrames[projectile.type] = 3;
      ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      projectile.width = 20;
      projectile.height = 20;
      projectile.aiStyle = -1;
      projectile.hostile = true;
      projectile.timeLeft = 240;
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      projectile.alpha = 0;
      projectile.hide = true;
      cooldownSlot = 1;
    }

    public override void AI()
    {
      if (projectile.ai[0] == 0.0 && projectile.timeLeft > 105)
        projectile.timeLeft = 105;
      if (projectile.localAI[0] == 0.0)
      {
        projectile.localAI[0] = 1f;
        projectile.scale = Main.rand.NextFloat(1.5f, 2f);
        projectile.frame = Main.rand.Next(3);
        projectile.hide = false;
        Main.PlaySound(SoundID.Item8, projectile.Center);
      }
      if (++projectile.localAI[1] > 30.0 && projectile.localAI[1] < 100.0)
      {
        Projectile projectile1 = projectile;
        projectile1.velocity *= 1.06f;
      }
      projectile.rotation += projectile.velocity.X * 0.01f;
      int index = Dust.NewDust(projectile.Center, 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index].noGravity = true;
      Main.dust[index].scale *= 2f;
      Dust dust = Main.dust[index];
      dust.velocity *= 0.1f;
      Dust.NewDust(projectile.position, projectile.width, projectile.height, 86, projectile.velocity.X, projectile.velocity.Y, 0, new Color(), 1f);
    }

    public override void Kill(int timeLeft)
    {
      if (projectile.ai[0] != 0.0 || Main.netMode == NetmodeID.MultiplayerClient)
        return;
      for (int index = -1; index <= 1; ++index)
        Projectile.NewProjectile(projectile.Center, Utils.RotatedBy(projectile.velocity, (double) MathHelper.ToRadians(5f) * index, new Vector2()) / 2f, projectile.type, projectile.damage, 0.0f, Main.myPlayer, 1f);
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[projectile.type]; ++index)
      {
        Color color = Color.White * projectile.Opacity * 0.75f * 0.5f;
        Vector2 oldPo = projectile.oldPos[index];
        float num3 = projectile.oldRot[index];
        Main.spriteBatch.Draw(texture2D, oldPo - projectile.Size / 2f - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(rectangle), color,num3, vector2, projectile.scale, spriteEffects, 0.0f);
      }
      Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, vector2, projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
