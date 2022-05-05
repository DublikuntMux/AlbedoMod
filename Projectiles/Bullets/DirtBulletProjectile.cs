using Albedo.Base;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Bullets
{
	public class DirtBulletProjectile : BasBulletProjectile
	{
		protected override string Name => "Dirt Bullet";
		protected override int Penetrate => 1;

		public override void AI()
        {
			projectile.aiStyle = 0;
			Lighting.AddLight(projectile.position, 0.2f, 0.2f, 0.6f);
			Lighting.Brightness(1, 1);
		}

        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 21, 0.5f, 0.8f);
			for (var i = 0; i < 6; i++)
            {
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 19, 0f, 0f, 0, default(Color), 1f);
            }
        }
	}
}
