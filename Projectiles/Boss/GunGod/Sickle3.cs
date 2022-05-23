using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Sickle3 : Sickle
	{
		public override string Texture => "Albedo/Projectiles/Boss/GunGod/Sickle";

		public override void AI()
		{
			if (projectile.localAI[0] == 0f) {
				projectile.localAI[0] = projectile.Center.X;
				projectile.localAI[1] = projectile.Center.Y;
				Main.PlaySound(SoundID.Item8, projectile.Center);
			}

			var projectile1 = projectile;
			projectile1.rotation += 0.8f;
			if (projectile.ai[1] == 0f) {
				var val = EnemyHelper.PlayerExists(projectile.ai[0]);
				if (val != null) {
					var vector = new Vector2(projectile.localAI[0], projectile.localAI[1]);
					if (projectile.Distance(vector) > val.Distance(vector) - 160f) {
						projectile.ai[1] = 1f;
						projectile.velocity.Normalize();
						projectile.timeLeft = 300;
						projectile.netUpdate = true;
					}
				}
			}
			else if ((projectile.ai[1] += 1f) < 60f) {
				var projectile2 = projectile;
				projectile2.velocity *= 1.065f;
			}
		}
	}
}