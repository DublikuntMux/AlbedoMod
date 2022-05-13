using Albedo.Buffs.Pets;
using Albedo.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.buffType = ModContent.BuffType<BulletPetBuff>();
        }
        
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return AlbedoUtils.CustomRarity(3027, line);
        }

        public override void UseStyle(Player player) {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
