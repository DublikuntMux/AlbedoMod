using Albedo.Base;
using Albedo.Items.Ammos.Bullets;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Pouches.Mod
{
	public class StonePouch : BasePouch
	{
		protected override int AmmunitionItem => ModContent.ItemType<StoneBullet>();
	}
}
