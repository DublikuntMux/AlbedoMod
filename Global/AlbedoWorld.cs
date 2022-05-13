using System.Collections.Generic;
using Albedo.Tiles.Ores;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Albedo.Global
{
    public class AlbedoWorld : ModWorld
    {
        public static bool DownedHellGuard;
        public static bool DownedGunGod;
        public static bool DownedGunDemon;
        
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (shiniestIndex != -1) {
                tasks.Insert(shiniestIndex + 1, new PassLegacy("Enigma Ores Gen", EnigmaOres));
            }
        }
        
        private static void EnigmaOres(GenerationProgress progress)
        {
            progress.Message = "Enigma World gen";
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++) {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Saltpeter>());
                x = WorldGen.genRand.Next(0, Main.maxTilesX);
                y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<AlbedoOreTitle>());
            }
        }
    }
}
