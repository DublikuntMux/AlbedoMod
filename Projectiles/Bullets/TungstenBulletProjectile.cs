using Albedo.Base;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Bullets
{
	public class TungstenBulletProjectile : BasBulletProjectile
	{
		protected override string Name => "Tungsten Bullet";
		protected override int Penetrate => 2;

		public override void AI()
		{
			Lighting.AddLight(projectile.position, 0.7f, 1.8f, 0.3f);
			Lighting.Brightness(1, 1);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Dig, (int) projectile.position.X, (int) projectile.position.Y, 21, 0.5f, 0.8f);
			for (int i = 0; i < 6; i++) Dust.NewDust(projectile.position, projectile.width, projectile.height, 1);
		}
	}
}