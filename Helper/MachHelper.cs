using Microsoft.Xna.Framework;
using Terraria;

namespace Albedo.Helper
{
	public class MachHelper
	{
		public static Vector2 RandomVelocity(float directionMult, float speedLowerLimit, float speedCap,
			float speedMult = 0.1f)
		{
			var vector = new Vector2(Main.rand.NextFloat(0f - directionMult, directionMult),
				Main.rand.NextFloat(0f - directionMult, directionMult));
			while (vector.X == 0f && vector.Y == 0f)
				vector = new Vector2(Main.rand.NextFloat(0f - directionMult, directionMult),
					Main.rand.NextFloat(0f - directionMult, directionMult));
			vector.Normalize();
			return vector * (Main.rand.NextFloat(speedLowerLimit, speedCap) * speedMult);
		}
	}
}