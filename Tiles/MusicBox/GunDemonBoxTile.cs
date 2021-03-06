using Albedo.Items.MusicBox;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Albedo.Tiles.MusicBox
{
	public class GunDemonBoxTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			AddMapEntry(Color.Orange);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) =>
			Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<GunDemonBox>());

		public override void MouseOver(int i, int j)
		{
			var player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ModContent.ItemType<GunDemonBox>();
		}
	}
}