using System.Collections.Generic;
using Albedo.Global;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Invasion
{
	public class InvasionNpc : GlobalNPC
	{
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			if (AlbedoWorld.GunInvasionUp && Main.invasionX == Main.spawnTileX) {
				pool.Clear();
				if (Main.hardMode) {
					if (Main.rand.Next(5) > 0)
						foreach (int i in GunInvasion.HardmodeInvaders)
							pool.Add(i, 1f);
					if (NPC.downedMoonlord)
						if (Main.rand.Next(5) > 0)
							foreach (int i in GunInvasion.PostLunarInvaders)
								pool.Add(i, 1f);
					foreach (int i in GunInvasion.PreHardmodeInvaders) pool.Add(i, 1f);
				}
				else {
					foreach (int i in GunInvasion.PreHardmodeInvaders) pool.Add(i, 1f);
				}
			}
		}

		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (AlbedoWorld.GunInvasionUp && Main.invasionX == Main.spawnTileX) {
				spawnRate = 20;
				maxSpawns = spawnRate * 1000000000;
			}
		}

		public override void PostAI(NPC npc)
		{
			if (AlbedoWorld.GunInvasionUp && Main.invasionX == Main.spawnTileX) npc.timeLeft = 2000;
		}

		public override void NPCLoot(NPC npc)
		{
			if (AlbedoWorld.GunInvasionUp) {
				int[] fullList = GunInvasion.GetFullInvaderList();
				foreach (int invader in fullList)
					if (npc.type == invader)
						Main.invasionSize -= 1;
			}
		}
	}
}