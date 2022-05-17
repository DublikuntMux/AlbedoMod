using Albedo.Base;
using Albedo.Helper;
using Albedo.Items.Materials;
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
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<AlbedoIngot>(), Main.rand.Next(20) + 20);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            var texture = mod.GetTexture("Items/TreasureBags/HellGuardBag_Glow");
            GameHelper.GlowMask(texture, rotation, scale, whoAmI);
        }
    }
}