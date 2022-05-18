using System;
using System.Collections.Generic;
using Albedo.Global;
using Albedo.Items.Materials;
using Albedo.Items.MusicBox;
using Albedo.Items.Summons;
using Albedo.Items.Trophies;
using Albedo.Items.Weapons.Ranged;
using Albedo.NPCs.Boss.GunDemon;
using Albedo.NPCs.Boss.GunGod;
using Albedo.NPCs.Boss.HellGuard;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo
{
	public class Albedo : Mod
	{
		public override void Load()
		{
			if (!Main.dedServ) {
				string[,] musicBoxes = {
					{"HellGuard", "HellGuardBox", "HellGuardBoxTile"},
					{"GunDemon", "GunDemonBox", "GunDemonBoxTile"},
					{"GunGod", "GunGodBox", "GunGodBoxTile"}
				};

				for (int i = 0; i < musicBoxes.GetUpperBound(0) + 1; i++)
					AddMusicBox(GetSoundSlot(SoundType.Music, $"Sounds/Music/{musicBoxes[i, 0]}"),
						ItemType(musicBoxes[i, 1]), TileType(musicBoxes[i, 2]));
			}
		}

		public override void PostSetupContent()
		{
			var bossChecklist = ModLoader.GetMod("BossChecklist");

			//SlimeKing = 1f;
			//EyeOfCthulhu = 2f;
			//EaterOfWorlds = 3f;
			//QueenBee = 4f;
			//Skeletron = 5f;
			//WallOfFlesh = 6f;
			//TheTwins = 7f;
			//TheDestroyer = 8f;
			//SkeletronPrime = 9f;
			//Plantera = 10f;
			//Golem = 11f;
			//DukeFishron = 12f;
			//LunaticCultist = 13f;
			//Moonlord = 14f;

			bossChecklist?.Call(
				"AddBoss",
				5.9f,
				ModContent.NPCType<HellGuard>(),
				this,
				Language.GetTextValue("Mods.Albedo.NPCName.HellGuard"),
				(Func<bool>) (() => AlbedoWorld.DownedHellGuard),
				ModContent.ItemType<SniperVoodooDoll>(),
				new List<int> {ModContent.ItemType<HellGuardTrophy>(), ModContent.ItemType<HellGuardBox>()},
				new List<int> {
					ItemID.GreaterHealingPotion, ModContent.ItemType<HellGuardSoul>(), ModContent.ItemType<LavaDisaster>(),
					ModContent.ItemType<Magmum>(), ModContent.ItemType<RustleDunes>()
				},
				Language.GetTextValue("Mods.Albedo.Boss.HellGuard.Info"),
				Language.GetTextValue("Mods.Albedo.Boss.HellGuard.Gone"),
				"Albedo/NPCs/Boss/BossChecklist/HellGuard");
			bossChecklist?.Call("AddBoss",
				10.3f,
				ModContent.NPCType<GunDemonHead>(),
				this,
				Language.GetTextValue("Mods.Albedo.NPCName.GunDemonHead"),
				(Func<bool>) (() => AlbedoWorld.DownedGunDemon),
				ModContent.ItemType<CursedPistol>(),
				new List<int> {ModContent.ItemType<GunDemonTrophy>(), ModContent.ItemType<GunDemonBox>()},
				new List<int> {ItemID.GreaterHealingPotion, ModContent.ItemType<GunDemonSoul>()},
				Language.GetTextValue("Mods.Albedo.Boss.GunDemon.Info"),
				Language.GetTextValue("Mods.Albedo.Boss.GunDemon.Gone"),
				"Albedo/NPCs/Boss/BossChecklist/GunDemon");
			bossChecklist?.Call("AddBoss",
				15f,
				ModContent.NPCType<GunGod>(),
				this,
				Language.GetTextValue("Mods.Albedo.NPCName.GunGod"),
				(Func<bool>) (() => AlbedoWorld.DownedGunGod),
				ModContent.ItemType<GodTribute>(),
				new List<int> {ModContent.ItemType<GunGodTrophy>(), ModContent.ItemType<GunGodBox>()},
				new List<int> {
					ItemID.SuperHealingPotion, ModContent.ItemType<GunGodSoul>(), ModContent.ItemType<SDFMG>(),
					ModContent.ItemType<ShotgunMeasurements>()
				},
				Language.GetTextValue("Mods.Albedo.Boss.GunGod.Info"),
				Language.GetTextValue("Mods.Albedo.Boss.GunGod.Gone"),
				"Albedo/NPCs/Boss/BossChecklist/GunGod");
		}
	}
}