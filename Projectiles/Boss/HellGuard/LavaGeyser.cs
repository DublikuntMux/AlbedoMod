using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.HellGuard
{
  public class LavaGeyser : ModProjectile
  {
    public override void SetStaticDefaults() => DisplayName.SetDefault("Geyser");

    public override void SetDefaults()
    {
      projectile.width = 2;
      projectile.height = 2;
      projectile.aiStyle = -1;
      projectile.hostile = true;
      projectile.penetrate = -1;
      projectile.timeLeft = 600;
      projectile.tileCollide = false;
      projectile.ignoreWater = true;
      projectile.hide = true;
      projectile.extraUpdates = 14;
    }

    public override bool CanDamage() => false;

    public override void AI()
    {
      Tile tileSafely = Framing.GetTileSafely(projectile.Center);
      if (projectile.ai[1] == 0.0)
      {
        projectile.position.Y -= 16f;
        if (!tileSafely.nactive() || !Main.tileSolid[tileSafely.type])
        {
          projectile.ai[1] = 1f;
          projectile.netUpdate = true;
        }
      }
      else if (tileSafely.nactive() && Main.tileSolid[tileSafely.type] && tileSafely.type != 19 && tileSafely.type != 380)
      {
        if (projectile.timeLeft > 90)
          projectile.timeLeft = 90;
        projectile.extraUpdates = 0;
        projectile.position.Y -= 16f;
        int index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, -8f, 0, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity *= 3f;
      }
      else
        projectile.position.Y += 16f;
      if (projectile.timeLeft > 120)
        return;
      Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 0, new Color(), 1f);
    }

    public override void Kill(int timeLeft)
    {
      if (Main.netMode == NetmodeID.MultiplayerClient)
        return;
      Projectile.NewProjectile(projectile.Center, Vector2.UnitY * -8f, 654, projectile.damage, 0.0f, Main.myPlayer);
    }
  }
}
