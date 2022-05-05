using Albedo.Base;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Bullets
{
	public class GemBulletProjectile : BasBulletProjectile
	{
		protected override string Name => "Gem Bullet";
		protected override int Penetrate => 3;

		public override void AI()
		{
			projectile.aiStyle = 0;
			Lighting.AddLight(projectile.position, 0.9f, 0.9f, 0.9f);
			Lighting.Brightness(1, 1);
			int dust = Dust.NewDust(projectile.Center, 1, 1, 15, 0f, 0f, 0, default(Color), 0.6f);
			Main.dust[dust].velocity *= 0.3f;
			Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f;
			Main.dust[dust].noGravity = true;
		}

        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 21, 0.5f, 0.8f);
			for (var i = 0; i < 20; i++)
            {
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 0, default(Color), 1f);
            }
        }
	}
}
