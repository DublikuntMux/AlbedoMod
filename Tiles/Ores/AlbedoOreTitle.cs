using Albedo.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Tiles.Ores
{
    public class AlbedoOreTitle : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 235;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(215, 15, 115), name);

            dustType = DustID.AmethystBolt;
            drop = ModContent.ItemType<AlbedoOre>();
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 2f;
            minPick = 45;
        }
    }
}
