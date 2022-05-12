using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Flocko3 : Flocko
	{
		public override string Texture => "Terraria/Cloud_17";

		public override void AI()
		{
			NPC val = AlbedoUtils.NpcExists(projectile.ai[0], ModContent.NPCType<NPCs.Boss.GunGod.GunGod>());
			if (val == null)
			{
				projectile.Kill();
				return;
			}
			Vector2 center = val.Center;
			center.X += projectile.ai[1];
			center.Y -= 1100f;
			Vector2 vector = center - projectile.Center;
			if (vector.Length() > 10f)
			{
				vector /= 8f;
				projectile.velocity = (projectile.velocity * 23f + vector) / 24f;
			}
			else if (projectile.velocity.Length() < 12f)
			{
				Projectile projectile1 = projectile;
				projectile1.velocity *= 1.05f;
			}
			if ((projectile.localAI[0] += 1f) > 180f && (projectile.localAI[1] += 1f) > ((val.localAI[3] > 1f) ? 4 : 2))
			{
				Main.PlaySound(SoundID.Item27, projectile.position);
				projectile.localAI[1] = 0f;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Vector2 vector2 = new Vector2(Main.rand.Next(-1000, 1001), Main.rand.Next(-1000, 1001));
					vector2.Normalize();
					vector2 *= 6f;
					vector2.X /= 2f;
					Projectile.NewProjectile(projectile.Center + vector2 * 4f, vector2, ModContent.ProjectileType<FrostShard>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
				}
			}
			Projectile projectile3 = projectile;
			if (++projectile3.frameCounter > 3)
			{
				Projectile projectile4 = projectile;
				if (++projectile4.frame >= 6)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
		}
	}
}
