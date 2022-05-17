using Albedo.Base;
using Albedo.Items.Materials;
using Albedo.Tiles.CraftStations;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Weapons.Ranged
{
    public class RustleDunes : AlbedoItem
    {
        protected override int Rarity => 6;

        public override void SetDefaults()
        {
            item.damage = 19;
            item.ranged = true;
            item.width = 48;
            item.height = 30;
            item.useTime = 3;
            item.reuseDelay = 8;
            item.useAnimation = 3;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 6f;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shootSpeed = 6f;
            item.shoot = ProjectileID.Bullet;
            item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 50);
            recipe.AddIngredient(ItemID.AntlionMandible, 15);
            recipe.AddIngredient(ItemID.Amber, 10);
            recipe.AddIngredient(ModContent.ItemType<AlbedoIngot>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Gunpowder>(), 15);
            recipe.AddTile(ModContent.TileType<WeaponStation1Tile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}