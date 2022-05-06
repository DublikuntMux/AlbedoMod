using Albedo.Items.Ammos.Pouches.Mod;
using Albedo.Items.Ammos.Pouches.Vanila;
using Albedo.Projectiles.Combined;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using static Terraria.Main;

namespace Albedo.Items.Ammos.Combined
{
    public class PrehardmodePouch : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 26;
            item.height = 26;
            item.knockBack = 8f;
            item.consumable = false;
            item.maxStack = 1;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<PrehardmodBulletProjectile>();
            item.shootSpeed = 15f;
            item.value = Item.buyPrice(14);
            item.ammo = AmmoID.Bullet;
        }
        
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (((TooltipLine)line).mod == "Terraria" && ((TooltipLine)line).Name == "ItemName")
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(2873), item, (DrawData?)null);
                Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1f, 0f, 0f, -1);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
                return false;
            }
            return true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe val = new ModRecipe(mod);
            val.AddIngredient(ModContent.ItemType<ExplosivePouch>());
            val.AddIngredient(ModContent.ItemType<GoldenPouch>());
            val.AddIngredient(ModContent.ItemType<MeteorPouch>());
            val.AddIngredient(ModContent.ItemType<SilverPouch>());
            val.AddIngredient(ModContent.ItemType<CopperPouch>());
            val.AddIngredient(ModContent.ItemType<DirtPouch>());
            val.AddIngredient(ModContent.ItemType<GelPouch>());
            val.AddIngredient(ModContent.ItemType<GemPouch>());
            val.AddIngredient(ModContent.ItemType<IcePouch>());
            val.AddIngredient(ModContent.ItemType<IronPouch>());
            val.AddIngredient(ModContent.ItemType<LeadPouch>());
            val.AddIngredient(ModContent.ItemType<SnowPouch>());
            val.AddIngredient(ModContent.ItemType<StonePouch>());
            val.AddIngredient(ModContent.ItemType<TungstenPouch>());
            val.AddTile(TileID.CrystalBall);
            val.SetResult(this, 1);
            val.AddRecipe();
        }
    }
}
