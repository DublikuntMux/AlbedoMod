using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
	public class Arena : BaseArena
	{
		public Arena()
			: base((float) Math.PI / 180f, 1400f, ModContent.NPCType<NPCs.Boss.HellGuard.HellGuard>(), 87)
		{
		}

		public override string Texture => "Terraria/Item_121";

		public override void SetStaticDefaults() => DisplayName.SetDefault("HellGuard Seal");

		protected override void Movement(NPC npc)
		{
			if (npc.ai[0] < 9f) {
				projectile.velocity = npc.Center - projectile.Center;
				if (npc.ai[0] != 8f) {
					var projectile1 = projectile;
					projectile1.velocity /= 40f;
				}

				rotationPerTick = (float) Math.PI / 180f;
			}
			else {
				projectile.velocity = Vector2.Zero;
				rotationPerTick = -0.0017453292f;
			}
		}

		public override void AI()
		{
			base.AI();
			var projectile1 = projectile;
			projectile1.rotation += 0.3f;
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			base.OnHitPlayer(player, damage, crit);
			player.AddBuff(BuffID.OnFire, 600);
		}
	}
}