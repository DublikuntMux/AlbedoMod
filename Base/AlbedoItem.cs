using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class AlbedoItem : ModItem
	{
		protected abstract int Rarity { get; }

		public override void SetDefaults()
		{
			switch (Rarity) {
				case 12:
				case 11:
					item.rare = ItemRarityID.Expert;
					break;
				case 10:
				case 9:
					item.rare = ItemRarityID.Purple;
					break;
				case 8:
				case 7:
				case 6:
					item.rare = ItemRarityID.Yellow;
					break;
				case 5:
					item.rare = ItemRarityID.Orange;
					break;
				case 4:
					item.rare = ItemRarityID.Green;
					break;
				case 3:
					item.rare = ItemRarityID.Blue;
					break;
				case 2:
					item.rare = ItemRarityID.White;
					break;
				case 1:
					item.rare = ItemRarityID.Gray;
					break;
			}
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (line.mod == "Terraria" && line.Name == "ItemName") {
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);

				switch (Rarity) {
					case 12:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.Purple)
							.UseSecondaryColor(Color.Red).Apply(); //RainbowMid
						break;
					case 11:
						GameShaders.Misc["PulseUpwards"].UseColor(Color.Purple)
							.UseSecondaryColor(Color.Red).Apply(); //Rainbow
						break;
					case 10:
						GameShaders.Misc["PulseCircle"].UseColor(Color.Purple)
							.UseSecondaryColor(Color.Aqua).Apply(); //Cosmos
						break;
					case 9:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.Purple)
							.UseSecondaryColor(Color.Aqua).Apply(); //Purple
						break;
					case 8:
						GameShaders.Misc["PulseCircle"].UseColor(Color.Gold)
							.UseSecondaryColor(Color.LightGoldenrodYellow).Apply(); //Gold
						break;
					case 7:
						GameShaders.Misc["PulseCircle"].UseColor(Color.Brown)
							.UseSecondaryColor(Color.SandyBrown).Apply(); //Copper
						break;
					case 6:
						GameShaders.Misc["PulseCircle"].UseColor(Color.Yellow)
							.UseSecondaryColor(Color.LightYellow).Apply(); //Yellow
						break;
					case 5:
						GameShaders.Misc["PulseCircle"].UseColor(new Color(255, 48, 154))
							.UseSecondaryColor(new Color(255, 169, 240)).Apply(); //Orange
						break;
					case 4:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.Green)
							.UseSecondaryColor(Color.ForestGreen).Apply(); //Green
						break;
					case 3:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.Blue)
							.UseSecondaryColor(Color.Aqua).Apply(); //Blue
						break;
					case 2:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.White)
							.UseSecondaryColor(Color.WhiteSmoke).Apply(); //White
						break;
					case 1:
						GameShaders.Misc["PulseDiagonal"].UseColor(Color.Black)
							.UseSecondaryColor(Color.DarkSlateGray).Apply(); //Black
						break;
				}

				Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y),
					new Color(255, 169, 240));
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
				return false;
			}

			return true;
		}
	}
}