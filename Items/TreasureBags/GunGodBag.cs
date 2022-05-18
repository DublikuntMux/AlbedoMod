using Albedo.Base;
using Albedo.Helper;
using Albedo.Items.Materials;
using Albedo.Items.MusicBox;
using Albedo.Items.Trophies;
using Albedo.Items.Weapons.Ranged;
using Albedo.NPCs.Boss.GunGod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Items.TreasureBags
{
	public class GunGodBag : AlbedoItem
	{
		protected override int Rarity => 11;

		public override int BossBagNPC => ModContent.NPCType<GunGod>();

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
		}

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<GunGodSoul>(), Main.rand.Next(16) + 15);
			if (Main.rand.Next(100) >= 10) player.QuickSpawnItem(ModContent.ItemType<GunGodTrophy>());
			if (Main.rand.Next(100) >= 10) player.QuickSpawnItem(ModContent.ItemType<GunGodBox>());
			switch (Main.rand.Next(4)) {
				case 1 :
					player.QuickSpawnItem(ModContent.ItemType<SDFMG>());
					break;
				case 2 :
					player.QuickSpawnItem(ModContent.ItemType<ShotgunMeasurements>());
					break;
				case 3 :
					break;
				case 4 :
					break;
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/TreasureBags/GunGodBag_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}