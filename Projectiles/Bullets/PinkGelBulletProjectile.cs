using Albedo.Base;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Bullets
{
	public class PinkGelBulletProjectile : BasBulletProjectile
	{
		protected override string Name => "PinkGel Bullet";
		protected override int Penetrate => 4;
	
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
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, 0f, 0f, 0, new Color(244,0,255), 1f);
            }
        }

        private int _bounce = 0;
        private const int MaxBounces = 5;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
	        _bounce++;
	        Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 21, 0.5f, 0.8f);
	        for (var i = 0; i < 4; i++)
	        {
		        Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, 0f, 0f, 0, new Color(244,0,255), 1f);
	        }
	        if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
	        if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y;
	        projectile.aiStyle = 1;

	        return _bounce >= MaxBounces;
        }
	}
}
