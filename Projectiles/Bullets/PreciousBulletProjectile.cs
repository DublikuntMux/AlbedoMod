using Albedo.Base;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Bullets
{
	public class PreciousBulletProjectile : BasBulletProjectile
	{
		protected override string Name => "Precious Bullet";
		protected override int Penetrate => 4;
	
		public override void AI()
        {
			projectile.aiStyle = 0;
			Lighting.AddLight(projectile.position, 0.7f, 0.7f, 0.7f);
			Lighting.Brightness(1, 1);
		}

        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 21, 0.5f, 0.8f);
			for (var i = 0; i < 6; i++)
            {
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 11, 0f, 0f, 0, default(Color), 1f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 10, 0f, 0f, 0, default(Color), 1f);
            }
        }
	}
}
