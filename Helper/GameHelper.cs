using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.Main;

namespace Albedo.Helper
{
	public static class GameHelper
	{
		public static void HomeInOnNPC(Projectile projectile, bool ignoreTiles, float distanceRequired,
			float homingVelocity, float n)
		{
			if (!projectile.friendly) return;
			int defExtraUpdates = projectile.extraUpdates;
			var center = projectile.Center;
			bool flag = false;
			for (int i = 0; i < 200; i++) {
				float num = npc[i].width / 2 + npc[i].height / 2;
				if (npc[i].CanBeChasedBy(projectile) && projectile.WithinRange(npc[i].Center, distanceRequired + num) &&
				    (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, npc[i].Center, 1, 1))) {
					center = npc[i].Center;
					flag = true;
					break;
				}
			}

			if (flag) {
				projectile.extraUpdates = defExtraUpdates + 1;
				var vector = (center - projectile.Center).SafeNormalize(Vector2.UnitY);
				projectile.velocity = (projectile.velocity * n + vector * homingVelocity) / (n + 1f);
			}
			else {
				projectile.extraUpdates = defExtraUpdates;
			}
		}

		public static void GlowMask(Texture2D texture, float rotation, float scale, int whoAmI)
		{
			var val = item[whoAmI];
			int num = texture.Height;
			int width = texture.Width;
			var effects = val.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			var rectangle = new Rectangle(0, 0, width, num);
			var vector = new Vector2(val.Center.X, val.position.Y + val.height - num / 2);
			spriteBatch.Draw(texture, vector - screenPosition, rectangle, Color.White, rotation, rectangle.Size() / 2f,
				scale, effects, 0f);
		}

		public static void Chat(string message, Color color, bool sync = true)
		{
			switch (netMode) {
				case NetmodeID.SinglePlayer:
				case NetmodeID.MultiplayerClient:
					NewText(message, color.R, color.G, color.B);
					break;
				default: {
					if (sync && netMode == NetmodeID.Server)
						NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message),
							new Color(color.R, color.G, color.B));

					break;
				}
			}
		}

		public static int FindClosestHostileNpcPrioritizingMinionFocus(Projectile projectile, float detectionRange,
			bool lineCheck = false, Vector2 center = default)
		{
			if (center == default) center = projectile.Center;
			var ownerMinionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNpc != null && ownerMinionAttackTargetNpc.CanBeChasedBy() &&
			    ownerMinionAttackTargetNpc.Distance(center) < detectionRange &&
			    (!lineCheck || Collision.CanHitLine(center, 0, 0, ownerMinionAttackTargetNpc.Center, 0, 0)))
				return ownerMinionAttackTargetNpc.whoAmI;
			return FindClosestHostileNPC(center, detectionRange, lineCheck);
		}

		private static int FindClosestHostileNPC(Vector2 location, float detectionRange, bool lineCheck = false)
		{
			NPC val = null;
			var npc = Main.npc;
			foreach (var val2 in npc)
				if (val2.CanBeChasedBy() && val2.Distance(location) < detectionRange &&
				    (!lineCheck || Collision.CanHitLine(location, 0, 0, val2.Center, 0, 0))) {
					detectionRange = val2.Distance(location);
					val = val2;
				}

			return val?.whoAmI ?? -1;
		}
	}
}