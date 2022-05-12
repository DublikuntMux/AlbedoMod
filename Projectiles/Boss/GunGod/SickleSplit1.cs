using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class SickleSplit1 : Sickle
	{
		public override string Texture => "Albedo/Projectiles/Boss/GunGod/Sickle";

		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.timeLeft = 90;
		}

		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				projectile.localAI[0] = 1f;
				Main.PlaySound(SoundID.Item8, projectile.Center);
			}
			Projectile projectile1 = projectile;
			projectile1.rotation += 0.8f;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector2 vector = Utils.RotatedBy(Vector2.Normalize(projectile.velocity), Math.PI / 4.0 * i, default(Vector2));
					Projectile.NewProjectile(projectile.Center, vector, ModContent.ProjectileType<SickleSplit2>(), projectile.damage, 0f, projectile.owner, 0f, 0f);
				}
			}
		}
	}
}
