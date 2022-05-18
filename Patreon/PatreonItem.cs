using System.Collections.Generic;
using Albedo.Base;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Albedo.Patreon
{
	public abstract class PatreonItem : AlbedoItem
	{
		protected abstract string Owner { get; }

		protected override int Rarity => 8;

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var val1 = new TooltipLine(mod, "tooltip", "=> Patreon <=") {
				overrideColor = Color.Orange
			};
			var val2 = new TooltipLine(mod, "tooltip", "By: " + Owner) {
				overrideColor = Color.Orange
			};
			tooltips.Add(val1);
			tooltips.Add(val2);
		}
	}
}