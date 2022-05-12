using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Albedo.Global
{
    public class AlbedoPlayer : ModPlayer
    {
        public bool BulletPet;
        public bool CanGrap;
        public int Screenshake;

        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
            {
                Main.screenPosition += Main.rand.NextVector2Circular(7f, 7f);
            }
        }

        public override void ResetEffects()
        {
            if (Screenshake > 0)
                --Screenshake;
        }

        public override void UpdateDead()
        {
            if (Screenshake > 0)
                --Screenshake;
        }
    }
}
