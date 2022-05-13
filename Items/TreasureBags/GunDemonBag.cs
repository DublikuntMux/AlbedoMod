using Albedo.Items.Materials;
using Albedo.NPCs.Boss.GunGod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Albedo.Items.TreasureBags
{
	public class GunDemonBag : ModItem
	{
		public override int BossBagNPC => ModContent.NPCType<GunGod>();

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Expert;
		}

		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<GunGodSoul>(), Main.rand.Next(16) + 15);
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/TreasureBags/GunDemonBag_Glow");
			AlbedoUtils.GlowMask(texture, rotation, scale, whoAmI);
		}
		
		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return AlbedoUtils.LiveRarity(3556, line);
		}
	}
}
