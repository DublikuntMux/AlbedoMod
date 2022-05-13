using Albedo.Buffs.Permanents;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Albedo.Global
{
    public class AlbedoPlayer : ModPlayer
    {
        public bool BulletPet;
        public bool CanGrap;
        public int Screenshake;

        public override void OnEnterWorld(Player player)
        {
            AlbedoUtils.Chat(Language.GetTextValue("Mods.Albedo.Misc.OnEnter"), Color.Red, false);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AlbedoWorld.DownedGunDemon)
            {
                player.AddBuff(ModContent.BuffType<HellConfessions>(), 3);
            }
        }
        
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
