using System.Collections.Generic;
using System.IO;
using Albedo.Items.Weapons.Ranged;
using Albedo.Tiles.Ores;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace Albedo.Global
{
	public class AlbedoWorld : ModWorld
	{
		public static bool DownedHellGuard;
		public static bool DownedGunDemon;
		public static bool DownedGunGod;

		public override TagCompound Save()
		{
			var list = new List<string>();
			if (DownedHellGuard) list.Add("HellGuard");
			if (DownedGunDemon) list.Add("GunDemon");
			if (DownedGunGod) list.Add("GunGod");
			var tc = new TagCompound {
				{"Downed", list}
			};
			return tc;
		}

		public override void Load(TagCompound tag)
		{
			var list = tag.GetList<string>("Downed");
			DownedHellGuard = list.Contains("HellGuard");
			DownedGunDemon = list.Contains("GunDemon");
			DownedGunGod = list.Contains("GunGod");
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte bitsByte = reader.ReadByte();
			DownedHellGuard = bitsByte[0];
			DownedGunDemon = bitsByte[1];
			DownedGunGod = bitsByte[2];
		}

		public override void NetSend(BinaryWriter writer)
		{
			var bitsByte = new BitsByte {
				[0] = DownedHellGuard,
				[1] = DownedGunDemon,
				[2] = DownedGunGod
			};
			writer.Write(bitsByte);
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int shiniestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (shiniestIndex != -1) tasks.Insert(shiniestIndex + 1, new PassLegacy("Albedo Ores gen", AlbedoOres));
		}

		private static void AlbedoOres(GenerationProgress progress)
		{
			progress.Message = "Albedo World gen";

			for (int k = 0; k < (int) (Main.maxTilesX * Main.maxTilesY * 6E-05); k++) {
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6),
					ModContent.TileType<Saltpeter>());
				x = WorldGen.genRand.Next(0, Main.maxTilesX);
				y = WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6),
					ModContent.TileType<AlbedoOreTitle>());
				x = WorldGen.genRand.Next(0, Main.maxTilesX);
				y = WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6),
					ModContent.TileType<AngeliteOreTile>());
			}
		}

		public override void PostWorldGen()
		{
			int[] jungleChestItems = {ModContent.ItemType<Musket>()};
			int jungleChestItemsChoice = 0;
			int[] iceChestItems = {ModContent.ItemType<KryonikGun>()};
			int iceChestItemsChoice = 0;

			for (int chestIndex = 0; chestIndex < 1000; chestIndex++) {
				var chest = Main.chest[chestIndex];
				if (Main.rand.NextFloat(0, 100) <= 30)
					if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers &&
					    Main.tile[chest.x, chest.y].frameX == 10 * 36)
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
							if (chest.item[inventoryIndex].type == ItemID.None) {
								chest.item[inventoryIndex].SetDefaults(jungleChestItems[jungleChestItemsChoice]);
								jungleChestItemsChoice = (jungleChestItemsChoice + 1) % jungleChestItems.Length;
								break;
							}

				if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers &&
				    Main.tile[chest.x, chest.y].frameX == 11 * 36)
					if (Main.rand.NextFloat(0, 100) <= 30)
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
							if (chest.item[inventoryIndex].type == ItemID.None) {
								chest.item[inventoryIndex].SetDefaults(iceChestItems[iceChestItemsChoice]);
								iceChestItemsChoice = (iceChestItemsChoice + 1) % iceChestItems.Length;
								break;
							}
			}
		}
	}
}