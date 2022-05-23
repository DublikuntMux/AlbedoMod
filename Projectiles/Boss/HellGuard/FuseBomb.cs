using Albedo.Global;
using Albedo.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
	public class FuseBomb : ModProjectile
	{
		public override string Texture => "Albedo/Projectiles/Empty";

		public override void SetDefaults()
		{
			projectile.width = 300;
			projectile.height = 300;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.hide = true;
			projectile.extraUpdates = 1;
			projectile.GetGlobalProjectile<AlbedoGloabalProjectile>().DeletionImmuneRank = 2;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (NPC.golemBoss != -1 && Main.npc[NPC.golemBoss].active && Main.npc[NPC.golemBoss].type == 245) {
				target.AddBuff(24, 600);
				target.AddBuff(36, 600);
				target.AddBuff(mod.BuffType("Defenseless"), 600);
				target.AddBuff(195, 600);
				if (Framing.GetTileSafely(Main.npc[NPC.golemBoss].Center).wall != 87)
					target.AddBuff(67, 120);
			}

			if (!EnemyHelper.BossIsAlive(ref AlbedoGlobalNpc.HellGuard,
				    ModContent.NPCType<NPCs.Boss.HellGuard.HellGuard>()))
				return;
			target.AddBuff(24, 300);
			target.AddBuff(67, 300);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item, (int) projectile.Center.X, (int) projectile.Center.Y, 14);
			for (int index1 = 0; index1 < 45; ++index1) {
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0.0f, 0.0f, 100,
					new Color(), 1.5f);
				var dust = Main.dust[index2];
				dust.velocity *= 1.4f;
			}

			for (int index3 = 0; index3 < 30; ++index3) {
				int index4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100,
					new Color(), 3.5f);
				Main.dust[index4].noGravity = true;
				var dust1 = Main.dust[index4];
				dust1.velocity *= 7f;
				int index5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100,
					new Color(), 1.5f);
				var dust2 = Main.dust[index5];
				dust2.velocity *= 3f;
			}

			for (int index6 = 0; index6 < 3; ++index6) {
				float num = 0.4f;
				if (index6 == 1)
					num = 0.8f;
				int index7 = Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64));
				var gore1 = Main.gore[index7];
				gore1.velocity *= num;
				++Main.gore[index7].velocity.X;
				++Main.gore[index7].velocity.Y;
				int index8 = Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64));
				var gore2 = Main.gore[index8];
				gore2.velocity *= num;
				--Main.gore[index8].velocity.X;
				++Main.gore[index8].velocity.Y;
				int index9 = Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64));
				var gore3 = Main.gore[index9];
				gore3.velocity *= num;
				++Main.gore[index9].velocity.X;
				--Main.gore[index9].velocity.Y;
				int index10 = Gore.NewGore(projectile.Center, new Vector2(), Main.rand.Next(61, 64));
				var gore4 = Main.gore[index10];
				gore4.velocity *= num;
				--Main.gore[index10].velocity.X;
				--Main.gore[index10].velocity.Y;
			}
		}
	}
}