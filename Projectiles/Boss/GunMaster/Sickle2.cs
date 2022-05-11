using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunMaster
{
	public class Sickle2 : Sickle
	{
		public override string Texture => "Albedo/Projectiles/Boss/GunMaster/Sickle";

		public override void AI()
		{
			if (this.projectile.localAI[0] == 0f)
			{
				this.projectile.localAI[0] = this.projectile.Center.X;
				this.projectile.localAI[1] = this.projectile.Center.Y;
				Main.PlaySound(SoundID.Item8, this.projectile.Center);
			}
			Projectile projectile = this.projectile;
			projectile.rotation += 0.8f;
			if (this.projectile.ai[1] == 0f)
			{
				Player val = AlbedoUtils.PlayerExists(this.projectile.ai[0]);
				if (val != null)
				{
					Vector2 vector = new Vector2(this.projectile.localAI[0], this.projectile.localAI[1]);
					if (((Entity)this.projectile).Distance(vector) > ((Entity)val).Distance(vector) - 160f)
					{
						this.projectile.ai[1] = 1f;
						this.projectile.velocity.Normalize();
						this.projectile.timeLeft = 300;
						this.projectile.netUpdate = true;
					}
				}
			}
			else if ((this.projectile.ai[1] += 1f) < 60f)
			{
				Projectile projectile2 = this.projectile;
				projectile2.velocity *= 1.065f;
			}
		}
	}
}
