using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using static Terraria.Main;

namespace Albedo.Items.Materials
{
	public class GunDemonSoul : AlbedoItem
	{
		protected override int Rarity => 8;

		public override void SetStaticDefaults()
		{
			RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			var refItem = new Item();
			refItem.SetDefaults(ItemID.SoulofSight);
			item.width = refItem.width;
			item.height = refItem.height;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 1);
		}

		public override void GrabRange(Player player, ref int grabRange) => grabRange *= 3;

		public override bool GrabStyle(Player player)
		{
			var vectorItemToPlayer = player.Center - item.Center;
			var movement = -vectorItemToPlayer.SafeNormalize(default) * 0.1f;
			item.velocity += movement;
			item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
			return true;
		}

		public override void PostUpdate() =>
			Lighting.AddLight(item.Center, Color.OrangeRed.ToVector3() * 0.55f * essScale);
	}
}