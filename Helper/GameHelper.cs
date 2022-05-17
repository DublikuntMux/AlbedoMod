using System;
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
            var defExtraUpdates = projectile.extraUpdates;
            var center = projectile.Center;
            var flag = false;
            for (var i = 0; i < 200; i++)
            {
                float num = npc[i].width / 2 + npc[i].height / 2;
                if (npc[i].CanBeChasedBy(projectile) && projectile.WithinRange(npc[i].Center, distanceRequired + num) &&
                    (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, npc[i].Center, 1, 1)))
                {
                    center = npc[i].Center;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                projectile.extraUpdates = defExtraUpdates + 1;
                var vector = (center - projectile.Center).SafeNormalize(Vector2.UnitY);
                projectile.velocity = (projectile.velocity * n + vector * homingVelocity) / (n + 1f);
            }
            else
            {
                projectile.extraUpdates = defExtraUpdates;
            }
        }

        public static void GlowMask(Texture2D texture, float rotation, float scale, int whoAmI)
        {
            var val = item[whoAmI];
            var num = texture.Height;
            var width = texture.Width;
            var effects = val.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var rectangle = new Rectangle(0, 0, width, num);
            var vector = new Vector2(val.Center.X, val.position.Y + val.height - num / 2);
            spriteBatch.Draw(texture, vector - screenPosition, rectangle, Color.White, rotation, rectangle.Size() / 2f,
                scale, effects, 0f);
        }

        public static void Chat(string message, Color color, bool sync = true)
        {
            switch (netMode)
            {
                case NetmodeID.SinglePlayer:
                case NetmodeID.MultiplayerClient:
                    NewText(message, color.R, color.G, color.B);
                    break;
                default:
                {
                    if (sync && netMode == NetmodeID.Server)
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message),
                            new Color(color.R, color.G, color.B));

                    break;
                }
            }
        }

        public static void DustRing(Vector2 location, int max, int dust, float speed, Color color = default,
            float scale = 1f, bool noLight = false)
        {
            for (var i = 0; i < max; i++)
            {
                var velocity = speed * Vector2.UnitY.RotatedBy((float) Math.PI * 2f / max * i);
                var num = Dust.NewDust(location, 0, 0, dust, 0f, 0f, 0, color);
                Main.dust[num].noLight = noLight;
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity = velocity;
                Main.dust[num].scale = scale;
            }
        }
    }
}