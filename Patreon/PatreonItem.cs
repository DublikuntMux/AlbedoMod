using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Patreon
{
    public abstract class PatreonItem : ModItem
    {
        protected abstract string Owner { get; }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Yellow;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return AlbedoUtils.LiveRarity(3027, line);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var val1 = new TooltipLine(mod, "tooltip", "=> Patreon Item <=")
            {
                overrideColor = Color.Orange
            };
            var val2 = new TooltipLine(mod, "tooltip", "By: " + Owner)
            {
                overrideColor = Color.Orange
            };
            tooltips.Add(val1);
            tooltips.Add(val2);
        }
    }
}