using System;
using Albedo.Base;
using Albedo.Global;
using Albedo.Helper;
using Albedo.Items.TreasureBags;
using Albedo.NPCs.Enemies.Hell;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.NPCs.Boss.GunDemon
{
	public class GunDemonHead : WormBase
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			NPCID.Sets.TechnicallyABoss[npc.type] = false;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 74;
			npc.height = 82;
			npc.lifeMax = 42000;
			npc.damage = 140;
			npc.defense = 50;
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Burning] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.boss = true;
			bossBag = ModContent.ItemType<GunDemonBag>();

			npc.HitSound = SoundID.NPCHit57;
			npc.DeathSound = SoundID.NPCDeath13;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GunDemon");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.damage = 210;
			npc.defense = 55;
			npc.lifeMax = 53550;
		}

		public override void AI()
		{
			SegmentBody();
			UpdatePosition();
			UpdateVelocity();
			SpawnAdds();
		}

		private void SpawnAdds()
		{
			if (Main.rand.NextBool(350))
				NPC.NewNPC((int) npc.Center.X - 70, (int) npc.Center.Y, ModContent.NPCType<MagmaLeechHead>());
		}

		private void UpdatePosition() => npc.position += npc.velocity;

		private void UpdateVelocity()
		{
			if ((int) (Main.time % 180) == 0) {
				float from = npc.AngleFrom(Main.player[npc.target].Center);
				npc.velocity = new Vector2((float) Math.Cos(from), (float) Math.Sin(from)) * -7;
				npc.netUpdate = true;
			}
		}

		private void SegmentBody()
		{
			if (JustSpawned) {
				int previous = npc.whoAmI;
				const int segments = 50;

				for (int i = 0; i < segments; i++) {
					int type =
						i < segments - 1
							? ModContent.NPCType<GunDemonBody>()
							: ModContent.NPCType<GunDemonTail>();


					int segmentWhoAmI = NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, type, ai1: previous,
						ai2: npc.whoAmI);
					var segment = Main.npc[segmentWhoAmI];
					segment.whoAmI = segmentWhoAmI;
					segment.realLife = npc.whoAmI;
					segment.active = true;
					previous = segmentWhoAmI;
				}

				JustSpawned = false;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(24, 300);
			target.AddBuff(67, 300);
		}

		public override void BossLoot(ref string name, ref int potionType) => potionType = 188;

		public override void NPCLoot()
		{
			if (!AlbedoWorld.DownedGunDemon) {
				AlbedoWorld.DownedGunDemon = true;
				GameHelper.Chat(Language.GetTextValue("Mods.Albedo.BossMassage.GunDemon"), Color.Purple);
			}

			if (Main.netMode == NetmodeID.Server)
				NetMessage.SendData(MessageID.WorldData);
			Item.NewItem(npc.Hitbox, ModContent.ItemType<GunDemonBag>());
		}
	}
}