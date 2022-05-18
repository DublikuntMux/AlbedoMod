using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class SickleSplit2 : Sickle
	{
		public override string Texture => "Albedo/Projectiles/Boss/GunGod/Sickle";

		public override void AI()
		{
			if (projectile.localAI[0] == 0f) {
				projectile.localAI[0] = 1f;
				Main.PlaySound(SoundID.Item8, projectile.Center);
			}

			var projectile1 = projectile;
			projectile1.rotation += 0.8f;
			if ((projectile.localAI[1] += 1f) > 30f && projectile.localAI[1] < 100f) {
				var projectile2 = projectile;
				projectile2.velocity *= 1.06f;
			}
		}
	}
}