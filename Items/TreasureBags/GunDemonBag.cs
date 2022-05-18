using Albedo.Base;
using Albedo.Helper;
using Albedo.Items.Materials;
using Albedo.Items.MusicBox;
using Albedo.Items.Trophies;
using Albedo.NPCs.Boss.GunGod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Items.TreasureBags
{
	public class GunDemonBag : AlbedoItem
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
			player.QuickSpawnItem(ModContent.ItemType<GunDemonSoul>(), Main.rand.Next(16) + 15);
			if (Main.rand.Next(100) >= 10) player.QuickSpawnItem(ModContent.ItemType<GunDemonTrophy>());
			if (Main.rand.Next(100) >= 10) player.QuickSpawnItem(ModContent.ItemType<GunDemonBox>());
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/TreasureBags/GunDemonBag_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}