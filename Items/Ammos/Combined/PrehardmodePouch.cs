using Albedo.Base;
using Albedo.Items.Ammos.Pouches.Mod;
using Albedo.Items.Ammos.Pouches.Vanila;
using Albedo.Projectiles.Combined;
using Albedo.Tiles.CraftStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Combined
{
    public class PrehardmodePouch : AlbedoItem
    {
        protected override int Rarity => 8;

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 26;
            item.height = 26;
            item.knockBack = 8f;
            item.consumable = false;
            item.maxStack = 1;
            item.shoot = ModContent.ProjectileType<PrehardmodBulletProjectile>();
            item.shootSpeed = 15f;
            item.value = Item.buyPrice(14);
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            var val = new ModRecipe(mod);
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
            val.AddTile(ModContent.TileType<WeaponStation1Tile>());
            val.SetResult(this);
            val.AddRecipe();
        }
    }
}