using Albedo.Base;
using Albedo.Helper;
using Albedo.Items.Materials;
using Albedo.Items.MusicBox;
using Albedo.Items.Trophies;
using Albedo.Items.Weapons.Ranged;
using Albedo.NPCs.Boss.HellGuard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Items.TreasureBags
{
	public class HellGuardBag : AlbedoItem
	{
		protected override int Rarity => 11;

		public override int BossBagNPC => ModContent.NPCType<HellGuard>();

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
		}

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<HellGuardSoul>(), Main.rand.Next(20) + 20);
			if (Main.rand.Next(100) <= 10) player.QuickSpawnItem(ModContent.ItemType<HellGuardTrophy>());
			if (Main.rand.Next(100) <= 10) player.QuickSpawnItem(ModContent.ItemType<HellGuardBox>());
			switch (Main.rand.Next(14)) {
				case 1:
					player.QuickSpawnItem(ModContent.ItemType<LavaDisaster>());
					break;
				case 2:
					player.QuickSpawnItem(ModContent.ItemType<Magmum>());
					break;
				case 3:
					player.QuickSpawnItem(ModContent.ItemType<RustleDunes>());
					break;
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
			float rotation, float scale, int whoAmI)
		{
			var texture = mod.GetTexture("Items/TreasureBags/HellGuardBag_Glow");
			GameHelper.GlowMask(texture, rotation, scale, whoAmI);
		}
	}
}