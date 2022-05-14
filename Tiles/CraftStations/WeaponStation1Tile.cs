using Albedo.Items.CraftStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Albedo.Tiles.CraftStations
{
    public class WeaponStation1Tile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[2] {17, 17};
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.addTile(Type);
            var name = CreateMapEntryName();
            name.SetDefault(Language.GetTextValue("Mods.Albedo.WeaponStationTile.Name"));
            AddMapEntry(new Color(190, 180, 40), name);
            disableSmartCursor = true;
            dustType = DustID.Amber;
            minPick = 20;
            mineResist = 3f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 17, j * 17, 47, 34, ModContent.ItemType<WeaponStation1>());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.8f;
            g = 0.0f;
            b = 0.0f;
        }
    }
}