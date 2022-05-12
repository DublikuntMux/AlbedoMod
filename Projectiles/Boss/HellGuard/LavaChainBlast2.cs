using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace Albedo.Projectiles.Boss.HellGuard
{
  public class EarthChainBlast2 : MoonLordSunBlast
  {
    public override string Texture => "Terraria/Projectile_687";

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Chain Blast");
      Main.projFrames[projectile.type] = Main.projFrames[645];
    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
      target.AddBuff(24, 300, true);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
    {
      Texture2D texture2D = Main.projectileTexture[projectile.type];
      int num1 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
      int num2 = num1 * projectile.frame;
      Rectangle rectangle = new Rectangle(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Utils.Size(rectangle) * 2f;
      Color color = projectile.ai[1] <= 3.0 ? Color.Lerp(new Color((int) byte.MaxValue, 95, 46, 50), new Color(150, 35, 0, 100), (float) ((3.0 - projectile.ai[1]) / 3.0)) : Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), new Color((int) byte.MaxValue, 95, 46, 50), Math.Min(2f, 7f - projectile.ai[1]) / 4f);
      Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0.0f, projectile.gfxOffY), new Rectangle?(rectangle), color, projectile.rotation, vector2, projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
