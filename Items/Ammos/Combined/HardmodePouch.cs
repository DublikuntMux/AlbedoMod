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
    public class HardmodePouch : AlbedoItem
    {
        protected override int Rarity => 9;

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 26;
            item.height = 26;
            item.knockBack = 8f;
            item.consumable = false;
            item.maxStack = 1;
            item.shoot = ModContent.ProjectileType<HardmodeBulletProjectile>();
            item.shootSpeed = 15f;
            item.value = Item.buyPrice(14);
            item.ammo = AmmoID.Bullet;
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