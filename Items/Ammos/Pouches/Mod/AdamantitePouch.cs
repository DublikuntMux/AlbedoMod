using Albedo.Base;
using Albedo.Items.Ammos.Bullets;
using Terraria.ModLoader;

namespace Albedo.Items.Ammos.Pouches.Mod
{
    public class AdamantitePouch : BasePouch
    {
        protected override int AmmunitionItem => ModContent.ItemType<AdamantiteBullet>();
    }
}