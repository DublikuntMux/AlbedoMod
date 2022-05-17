using Albedo.Items.Trophies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Albedo.Tiles.Trophies
{
    public class HellGuardTrophyTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);
            disableSmartCursor = true;
            var name = CreateMapEntryName();
            name.SetDefault(Language.GetTextValue("Mods.Albedo.ItemName.HellGuardTrophy"));
            AddMapEntry(new Color(120, 0, 0), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 48, ModContent.ItemType<HellGuardTrophy>());
        }
    }
}