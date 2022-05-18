using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Sickle2 : Sickle
	{
		public override string Texture => "Albedo/Projectiles/Boss/GunGod/Sickle";

		public override void AI()
		{
			if (this.projectile.localAI[0] == 0f) {
				this.projectile.localAI[0] = this.projectile.Center.X;
				this.projectile.localAI[1] = this.projectile.Center.Y;
				Main.PlaySound(SoundID.Item8, this.projectile.Center);
			}

			var projectile = this.projectile;
			projectile.rotation += 0.8f;
			if (this.projectile.ai[1] == 0f) {
				var val = BossHelper.PlayerExists(this.projectile.ai[0]);
				if (val != null) {
					var vector = new Vector2(this.projectile.localAI[0], this.projectile.localAI[1]);
					if (this.projectile.Distance(vector) > val.Distance(vector) - 160f) {
						this.projectile.ai[1] = 1f;
						this.projectile.velocity.Normalize();
						this.projectile.timeLeft = 300;
						this.projectile.netUpdate = true;
					}
				}
			}
			else if ((this.projectile.ai[1] += 1f) < 60f) {
				var projectile2 = this.projectile;
				projectile2.velocity *= 1.065f;
			}
		}
	}
}