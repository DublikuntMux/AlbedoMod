using Terraria;
using Terraria.ModLoader;

namespace Albedo.Helper
{
	public class SpawnHelper
	{
		public static bool NoInvasion(NPCSpawnInfo spawnInfo) =>
			!spawnInfo.invasion &&
			((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface ||
			 Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);

		public static bool NoBiome(NPCSpawnInfo spawnInfo)
		{
			var player = spawnInfo.player;
			return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson &&
			       !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
		}

		public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo) =>
			!spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;

		public static bool NoZone(NPCSpawnInfo spawnInfo) => NoZoneAllowWater(spawnInfo) && !spawnInfo.water;

		public static bool NormalSpawn(NPCSpawnInfo spawnInfo) => !spawnInfo.playerInTown && NoInvasion(spawnInfo);

		#region Combined

		public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo) => NormalSpawn(spawnInfo) && NoZone(spawnInfo);

		public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo) =>
			NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);

		public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo) =>
			NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);

		#endregion
	}
}