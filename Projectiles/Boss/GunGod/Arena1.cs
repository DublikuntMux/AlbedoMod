using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Projectiles.Boss.GunGod
{
	public class Arena1 : BaseArena
	{
		public override string Texture => "Terraria/Item_98";

		public Arena1()
			: base((float)Math.PI / 180f, 1400f, ModContent.NPCType<NPCs.Boss.GunGod.GunGod>(), 87)
		{
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("GunGod Seal");
		}

		protected override void Movement(NPC npc)
		{
			if (npc.ai[0] < 9f)
			{
				projectile.velocity = npc.Center - projectile.Center;
				if (npc.ai[0] != 8f)
				{
					Projectile projectile1 = projectile;
					projectile1.velocity /= 40f;
				}
				rotationPerTick = (float)Math.PI / 180f;
			}
			else
			{
				projectile.velocity = Vector2.Zero;
				rotationPerTick = -0.0017453292f;
			}
		}

		public override void AI()
		{
			base.AI();
			Projectile projectile1 = projectile;
			projectile1.rotation += 0.3f;
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			base.OnHitPlayer(player, damage, crit);
			player.AddBuff(30, 600, true);
		}
	}
}
