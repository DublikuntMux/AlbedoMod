using Albedo.Base;
using Albedo.Helper;
using Albedo.Invasion;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Albedo.Items.Summons
{
	public class VibrateGun : AlbedoItem
	{
		protected override int Rarity => 8;

		public override void SetStaticDefaults() => Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 22;
			item.scale = 1;
			item.maxStack = 99;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.NPCDeath44;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(0, 1);
			item.consumable = true;
		}

		public override bool CanUseItem(Player player) => !EnemyHelper.OtherBossAlive(0);

		public override bool UseItem(Player player)
		{
			GameHelper.Chat(Language.GetTextValue("Mods.Albedo.Invasion.Gun.UseItem"), Color.Coral);
			GunInvasion.StartDungeonInvasion();
			return true;
		}
	}
}