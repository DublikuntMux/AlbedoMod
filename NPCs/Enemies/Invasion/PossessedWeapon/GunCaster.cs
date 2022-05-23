using System;
using Albedo.Projectiles.Enemies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.NPCs.Enemies.Invasion.PossessedWeapon
{
	public class GunCaster : ModNPC
	{
		private bool _casting;

		private float ShootTimer {
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}

		private float TeleportTimer {
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}

		private float TpLocX {
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}

		private float TpLocY {
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}

		public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = 2;

		public override void SetDefaults()
		{
			npc.width = 30;
			npc.height = 48;
			npc.damage = 45;
			npc.defense = 30;
			npc.lifeMax = 500;
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 20);
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 900;
			npc.defense = 40;
		}

		public override void AI()
		{
			npc.TargetClosest();
			var val = Main.player[npc.target];
			var vector = val.Center - npc.Center;
			npc.spriteDirection = Math.Sign(vector.X);
			npc.velocity.X = 0f;
			if (ShootTimer > 0f) ShootTimer -= 1f;
			_casting = ShootTimer <= 30f;
			if (Main.netMode != NetmodeID.MultiplayerClient && ShootTimer <= 0f) {
				Main.PlaySound(SoundID.Item, (int) npc.position.X, (int) npc.position.Y, 20);
				const float num7 = 2 * (float) Math.PI / 10;
				int num8 = NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y,
					ModContent.NPCType<GunCusterMinion>(), 0, npc.whoAmI, 0f, num7);
				if (num8 != 200 && Main.netMode == NetmodeID.Server)
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8);

				ShootTimer = 60 * 3;
			}

			TeleportTimer--;
			if (TeleportTimer <= 0f) {
				TeleportTimer = 400f;
				Teleport(val, 0);
			}
		}

		private void Teleport(Entity p, int attemptNum)
		{
			while (true) {
				int num = (int) p.position.X / 16;
				int num2 = (int) p.position.Y / 16;
				int num3 = (int) npc.position.X / 16;
				int num4 = (int) npc.position.Y / 16;
				const int num5 = 20;
				bool flag = false;
				int num6 = Main.rand.Next(num - num5, num + num5);
				for (int i = Main.rand.Next(num2 - num5, num2 + num5); i < num2 + num5; i++) {
					if ((i >= num2 - 4 && i <= num2 + 4 && num6 >= num - 4 && num6 <= num + 4) ||
					    (i >= num4 - 1 && i <= num4 + 1 && num6 >= num3 - 1 && num6 <= num3 + 1) ||
					    !Main.tile[num6, i].nactive()) continue;

					bool flag2 = !Main.tile[num6, i - 1].lava();
					if (flag2 && Main.tileSolid[Main.tile[num6, i].type] &&
					    !Collision.SolidTiles(num6 - 1, num6 + 1, i - 4, i - 1)) {
						TpLocX = num6;
						TpLocY = i;
						flag = true;
						break;
					}
				}

				Main.PlaySound(SoundID.Item8, npc.position);
				if (TpLocX != 0f && TpLocY != 0f && flag) {
					npc.position.X = (float) (TpLocX * 16.0 - npc.width / 2 + 8.0);
					npc.position.Y = TpLocY * 16f - npc.height;
					npc.netUpdate = true;
					for (int k = 0; k < 20; k++) {
						var obj2 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.Vortex)];
						obj2.noGravity = true;
						obj2.scale = 1f;
						obj2.velocity *= 0.1f;
					}
				}
				else if (attemptNum < 10) {
					attemptNum += 1;
					continue;
				}

				break;
			}
		}

		public override void FindFrame(int frameHeight)
		{
			switch (_casting) {
				case false:
					npc.frame.Y = 0;
					break;
				case true:
					npc.frame.Y = frameHeight;
					break;
			}
		}
	}
}