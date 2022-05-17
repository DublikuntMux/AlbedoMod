using Albedo.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Albedo.Tiles.Bars
{
    public class AngeliteBarTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 191, 255));
        }

        public override bool Drop(int i, int j)
        {
            var t = Main.tile[i, j];
            var style = t.frameX / 18;
            if (style == 0) Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<AngeliteBar>());
            return base.Drop(i, j);
        }
    }
}