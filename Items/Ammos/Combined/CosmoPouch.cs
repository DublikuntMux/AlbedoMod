using Albedo.Projectiles.Combined;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Albedo.Items.Ammos.Combined
{
    public class CosmoPouch : ModItem
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
            item.shoot = ModContent.ProjectileType<CosmoBulletProjectile>();
            item.shootSpeed = 15f;
            item.value = Item.buyPrice(50);
            item.ammo = AmmoID.Bullet;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(2870), item);
                Utils.DrawBorderString(spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            var val = new ModRecipe(mod);
            val.AddIngredient(ModContent.ItemType<PrehardmodePouch>());
            val.AddIngredient(ModContent.ItemType<HardmodePouch>());
            val.AddIngredient(ModContent.ItemType<PostLunarPouch>());
            val.AddTile(ModContent.TileType<WeaponStation3Tile>());
            val.SetResult(this);
            val.AddRecipe();
        }
    }
}