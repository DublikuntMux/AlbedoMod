using System.Linq;
using Albedo.Global;
using Albedo.Helper;
using Albedo.NPCs.Enemies.Invasion.PossessedWeapon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.Invasion
{
	public class GunInvasion
	{
		public static readonly int[] PreHardmodeInvaders = {
			NPCID.BlazingWheel,
			NPCID.PossessedArmor,
			ModContent.NPCType<LiveArmorGold>(),
			ModContent.NPCType<LiveArmorCopper>()
		};

		public static readonly int[] HardmodeInvaders = {
			NPCID.MartianTurret,
			NPCID.EnchantedSword,
			NPCID.CrimsonAxe,
			NPCID.CursedHammer
		};

		public static readonly int[] PostLunarInvaders = {
			ModContent.NPCType<GunCaster>()
		};

		public static int[] GetFullInvaderList()
		{
			int[] list = new int[PreHardmodeInvaders.Length + HardmodeInvaders.Length + PostLunarInvaders.Length];

			PreHardmodeInvaders.CopyTo(list, 0);
			HardmodeInvaders.CopyTo(list, PreHardmodeInvaders.Length);
			PostLunarInvaders.CopyTo(list, HardmodeInvaders.Length);

			return list;
		}

		public static void StartDungeonInvasion()
		{
			if (Main.invasionType != 0 && Main.invasionSize == 0) Main.invasionType = 0;
			if (Main.invasionType == 0) {
				int num = 0;
				for (int i = 0; i < 255; i++)
					if (Main.player[i].active && Main.player[i].statLifeMax >= 500)
						num++;
				if (num > 0) {
					Main.invasionType = -1;
					AlbedoWorld.GunInvasionUp = true;
					Main.invasionSize = 200 * num;
					Main.invasionSizeStart = Main.invasionSize;
					Main.invasionProgress = 0;
					Main.invasionProgressIcon = 0 + 3;
					Main.invasionProgressWave = 0;
					Main.invasionProgressMax = Main.invasionSizeStart;
					Main.invasionWarn = 360;
					if (Main.rand.NextBool(2)) {
						Main.invasionX = 0.0;
						return;
					}

					Main.invasionX = Main.maxTilesX;
				}
			}
		}

		private static void DungeonInvasionWarning()
		{
			string text = "";
			if (Main.invasionX == Main.spawnTileX) text = Language.GetTextValue("Mods.Albedo.Invasion.Gun.Gona1");
			if (Main.invasionSize <= 0) text = Language.GetTextValue("Mods.Albedo.Invasion.Gun.Gona2");
			GameHelper.Chat(text, Color.Beige);
		}

		public static void UpdateDungeonInvasion()
		{
			if (AlbedoWorld.GunInvasionUp) {
				if (Main.invasionSize <= 0) {
					AlbedoWorld.GunInvasionUp = false;
					DungeonInvasionWarning();
					Main.invasionType = 0;
					Main.invasionDelay = 0;
				}

				if (Main.invasionX == Main.spawnTileX) return;

				float num = Main.dayRate;
				if (Main.invasionX > Main.spawnTileX) {
					Main.invasionX -= num;
					if (Main.invasionX <= Main.spawnTileX) {
						Main.invasionX = Main.spawnTileX;
						DungeonInvasionWarning();
					}
					else {
						Main.invasionWarn--;
					}
				}
				else {
					if (Main.invasionX < Main.spawnTileX) {
						Main.invasionX += num;
						if (Main.invasionX >= Main.spawnTileX) {
							Main.invasionX = Main.spawnTileX;
							DungeonInvasionWarning();
						}
						else {
							Main.invasionWarn--;
						}
					}
				}
			}
		}

		public static void CheckDungeonInvasionProgress()
		{
			int[] fullList = GetFullInvaderList();

			if (Main.invasionProgressMode != 2) {
				Main.invasionProgressNearInvasion = false;
				return;
			}

			bool flag = false;
			var rectangle = new Rectangle((int) Main.screenPosition.X, (int) Main.screenPosition.Y, Main.screenWidth,
				Main.screenHeight);
			const int num = 5000;
			int icon = 0;
			for (int i = 0; i < 200; i++)
				if (Main.npc[i].active) {
					icon = 0;
					int type = Main.npc[i].type;
					if ((from t in fullList
						    where type == t
						    select new Rectangle((int) (Main.npc[i].position.X + Main.npc[i].width / 2) - num,
							    (int) (Main.npc[i].position.Y + Main.npc[i].height / 2) - num, num * 2, num * 2))
					    .Any(value => rectangle.Intersects(value))) flag = true;
				}

			Main.invasionProgressNearInvasion = flag;
			int progressMax3 = 1;
			if (AlbedoWorld.GunInvasionUp) progressMax3 = Main.invasionSizeStart;
			if (AlbedoWorld.GunInvasionUp && Main.invasionX == Main.spawnTileX)
				Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);

			foreach (var p in Main.player)
				NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null,
					Main.invasionSizeStart - Main.invasionSize, Main.invasionSizeStart, Main.invasionType + 3);
		}
	}
}