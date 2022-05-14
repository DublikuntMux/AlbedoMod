using Albedo.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Tiles.Ores
{
    public class Saltpeter : ModTile
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

            var name = CreateMapEntryName();
            AddMapEntry(new Color(200, 200, 55), name);

            dustType = DustID.Amber;
            drop = ModContent.ItemType<RawSaltpeter>();
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 2f;
            minPick = 45;
        }
    }
}