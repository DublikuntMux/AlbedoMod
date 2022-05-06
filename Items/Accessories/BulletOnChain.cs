using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using static Terraria.Main;

namespace Albedo.Items.Accessories
{
    public class BulletOnChain : ModItem
    {
        public override void SetDefaults() {
            item.damage = 0;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 30;
            item.height = 30;
            item.UseSound = SoundID.Item2;
            item.useAnimation = 20;
            item.useTime = 20;
            item.rare = ItemRarityID.Yellow;
            item.noMelee = true;
            item.value = Item.sellPrice(gold:5, silver:50);
            item.shoot = ModContent.ProjectileType<Projectiles.Pets.BulletPet>();
            item.buffType = ModContent.BuffType<Buffs.BulletPetBuff>();
        }
        
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3027), item, (DrawData?)null);
                Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
                return false;
            }
            return true;
        }

        public override void UseStyle(Player player) {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
