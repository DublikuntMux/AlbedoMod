using Albedo.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Tiles.Ores
{
    public class AngeliteOreTile : ModTile
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
            AddMapEntry(new Color(0, 300, 900));

            dustType = DustID.GreenFairy;
            drop = ModContent.ItemType<AngeliteOre>();
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 2f;
            minPick = 225;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 0.3f;
            b = 0.9f;
        }
    }
}