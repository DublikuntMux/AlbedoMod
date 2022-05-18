using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Bullets
{
	public class PinkGelBulletProjectile : BasBulletProjectile
	{
		private const int MaxBounces = 5;

		private int _bounce;
		protected override string Name => "PinkGel Bullet";
		protected override int Penetrate => 4;

		public override void AI()
		{
			Lighting.AddLight(projectile.position, 0.2f, 0.2f, 0.6f);
			Lighting.Brightness(1, 1);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Dig, (int) projectile.position.X, (int) projectile.position.Y, 21, 0.5f, 0.8f);
			for (int i = 0; i < 6; i++)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, 0f, 0f, 0,
					new Color(244, 0, 255));
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			_bounce++;
			Main.PlaySound(SoundID.Dig, (int) projectile.position.X, (int) projectile.position.Y, 21, 0.5f, 0.8f);
			for (int i = 0; i < 4; i++)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, 0f, 0f, 0,
					new Color(244, 0, 255));
			if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
			if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y;
			projectile.aiStyle = 1;

			return _bounce >= MaxBounces;
		}
	}
}