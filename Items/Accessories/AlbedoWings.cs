using Albedo.Base;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class AlbedoWings : AlbedoItem
	{
		protected override int Rarity => 10;

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 36;
			item.value = Item.sellPrice(0, 30);
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.ignoreWater = true;
			player.accRunSpeed = 8.75f;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) => player.wingTimeMax = 950400;

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			player.wingsLogic = 22;
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 11f;
			acceleration *= 2.5f;
		}
	}
}