using Albedo.Items.Ammos.Pouches.Mod;
using Albedo.Items.Ammos.Pouches.Vanila;
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
    public class HardmodePouch : ModItem
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
            item.shoot = ModContent.ProjectileType<HardmodeBulletProjectile>();
            item.shootSpeed = 15f;
            item.value = Item.buyPrice(14);
            item.ammo = AmmoID.Bullet;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(3534), item);
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
            val.AddIngredient(ModContent.ItemType<PreciousPouch>());
            val.AddIngredient(ModContent.ItemType<VenomPouch>());
            val.AddIngredient(ModContent.ItemType<HellPouch>());
            val.AddIngredient(ModContent.ItemType<PartyPouch>());
            val.AddIngredient(ModContent.ItemType<PinkGelPouch>());
            val.AddIngredient(ModContent.ItemType<CursedPouch>());
            val.AddIngredient(ModContent.ItemType<IchorPouch>());
            val.AddIngredient(ModContent.ItemType<ChlorophytePouch>());
            val.AddIngredient(ModContent.ItemType<CrystalPouch>());
            val.AddIngredient(ModContent.ItemType<NanoPouch>());
            val.AddIngredient(ModContent.ItemType<VelocityPouch>());
            val.AddIngredient(ModContent.ItemType<AdamantitePouch>());
            val.AddIngredient(ModContent.ItemType<CobaltPouch>());
            val.AddIngredient(ModContent.ItemType<MythrilPouch>());
            val.AddTile(ModContent.TileType<WeaponStation2Tile>());
            val.SetResult(this);
            val.AddRecipe();
        }
    }
}