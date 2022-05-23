using System;
using System.Collections.Generic;
using Albedo.Global;
using Albedo.Items.Accessories;
using Albedo.Items.Materials;
using Albedo.Items.MusicBox;
using Albedo.Items.Summons;
using Albedo.Items.Trophies;
using Albedo.Items.Weapons.Ranged;
using Albedo.NPCs.Boss.GunDemon;
using Albedo.NPCs.Boss.GunGod;
using Albedo.NPCs.Boss.HellGuard;
using Albedo.NPCs.Enemies.Invasion.PossessedWeapon;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo
{
	public class Albedo : Mod
	{
		public Albedo()
		{
			Properties = new ModProperties {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

		public override void Load()
		{
			if (Main.netMode != NetmodeID.Server) {
				var shader1 = new Ref<Effect>(GetEffect("Effects/TextShader"));
				var shader2 = new Ref<Effect>(GetEffect("Effects/ScreenEffects"));

				GameShaders.Misc["PulseUpwards"] = new MiscShaderData(shader1, "PulseUpwards");
				GameShaders.Misc["PulseDiagonal"] = new MiscShaderData(shader1, "PulseDiagonal");
				GameShaders.Misc["PulseCircle"] = new MiscShaderData(shader1, "PulseCircle");

				Filters.Scene["InvertColor"] =
					new Filter(new ScreenShaderData(shader2, "InvertColor"), EffectPriority.VeryHigh);

				Filters.Scene["InvertColor"].Load();
			}

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
			var yabhb = ModLoader.GetMod("FKBossHealthBar");

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
					ItemID.GreaterHealingPotion, ModContent.ItemType<HellGuardSoul>(),
					ModContent.ItemType<LavaDisaster>(),
					ModContent.ItemType<Magmum>(), ModContent.ItemType<RustleDunes>()
				},
				Language.GetTextValue("Mods.Albedo.Boss.HellGuard.Info"),
				Language.GetTextValue("Mods.Albedo.Boss.HellGuard.Gone"),
				"Albedo/UI/BossChecklist/HellGuard");
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
				"Albedo/UI/BossChecklist/GunDemon");
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
					ModContent.ItemType<ShotgunMeasurements>(), ModContent.ItemType<GodImetator>()
				},
				Language.GetTextValue("Mods.Albedo.Boss.GunGod.Info"),
				Language.GetTextValue("Mods.Albedo.Boss.GunGod.Gone"),
				"Albedo/UI/BossChecklist/GunGod");
			
			bossChecklist?.Call("AddEvent", 9.6f, new List<int> {
					NPCID.BlazingWheel, NPCID.PossessedArmor, ModContent.NPCType<GunCaster>(), NPCID.MartianTurret,
					NPCID.EnchantedSword, NPCID.CrimsonAxe, NPCID.CursedHammer, ModContent.NPCType<LiveArmorCopper>(),
					ModContent.NPCType<LiveArmorGold>()
				},
				this,
				Language.GetTextValue("Mods.Albedo.Invasion.Gun.Name"),
				(Func<bool>) (() => AlbedoWorld.DownedGunInvasion),
				ModContent.ItemType<VibrateGun>(),
				new List<int>(),
				new List<int>(),
				Language.GetTextValue("Mods.Albedo.Invasion.Gun.Info"),
				Language.GetTextValue("Mods.Albedo.Invasion.Gun.Gona2"),
				"Albedo/UI/BossChecklist/GunInvaidors"
			);

			yabhb?.Call("RegisterCustomHealthBar",
				ModContent.NPCType<HellGuard>(),
				false,
				Language.GetTextValue("Mods.Albedo.NPCName.HellGuard"),
				GetTexture("UI/yabhb/ClassicBarFill"),
				GetTexture("UI/yabhb/HellBarStart"),
				GetTexture("UI/yabhb/HellBarMiddle"),
				GetTexture("UI/yabhb/HellBarEnd"));
			yabhb?.Call("RegisterCustomHealthBar",
				ModContent.NPCType<GunDemonHead>(),
				false,
				Language.GetTextValue("Mods.Albedo.NPCName.GunDemonHead"),
				GetTexture("UI/yabhb/ClassicBarFill"),
				GetTexture("UI/yabhb/HellBarStart"),
				GetTexture("UI/yabhb/HellBarMiddle"),
				GetTexture("UI/yabhb/HellBarEnd"));
			yabhb?.Call("RegisterCustomHealthBar",
				ModContent.NPCType<GunGod>(),
				false,
				Language.GetTextValue("Mods.Albedo.NPCName.GunGod"),
				GetTexture("UI/yabhb/GunBarFill"),
				GetTexture("UI/yabhb/GunBarStart"),
				GetTexture("UI/yabhb/GunBarMiddle"),
				GetTexture("UI/yabhb/GunBarEnd"));
		}
	}
}