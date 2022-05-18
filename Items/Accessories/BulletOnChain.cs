using Albedo.Base;
using Albedo.Buffs.Pets;
using Albedo.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Items.Accessories
{
	public class BulletOnChain : AlbedoItem
	{
		protected override int Rarity => 8;

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 30;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.noMelee = true;
			item.value = Item.sellPrice(gold: 5, silver: 50);
			item.shoot = ModContent.ProjectileType<BulletPet>();
			item.buffType = ModContent.BuffType<BulletPetBuff>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) player.AddBuff(item.buffType, 3600);
		}
	}
}