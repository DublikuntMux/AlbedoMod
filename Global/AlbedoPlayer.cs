using System;
using Albedo.Helper;
using Albedo.Projectiles.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Albedo.Global
{
	public class AlbedoPlayer : ModPlayer
	{
		public static bool StartMessage;

		public bool BulletPet;
		
		public bool GodImitator;
		public bool GunDemonCurse;
		public bool GunGodCurse;

		public bool HellGuardCurse;

		public int ScreenShake;

		public override void OnEnterWorld(Player player)
		{
			if (StartMessage) GameHelper.Chat(Language.GetTextValue("Mods.Albedo.Misc.OnEnter"), Color.Red, false);
		}

		public override void ModifyScreenPosition()
		{
			if (ScreenShake > 0) Main.screenPosition += Main.rand.NextVector2Circular(7f, 7f);
		}

		public override void ResetEffects()
		{
			if (ScreenShake > 0)
				--ScreenShake;
			BulletPet = false;
			HellGuardCurse = false;
			GunDemonCurse = false;
			GunGodCurse = false;
			GodImitator = false;
		}

		public override void UpdateDead()
		{
			if (ScreenShake > 0)
				--ScreenShake;
			HellGuardCurse = false;
			GunDemonCurse = false;
			GunGodCurse = false;
		}

		public override void PostUpdateBuffs()
		{
			if (GunDemonCurse) {
				player.lifeRegen = 0;
				player.lifeRegenTime = 0;
				player.allDamage /= 1.3f;
				player.statDefense -= 10;
				player.endurance /= 1.1f;
			}

			if (GunGodCurse) {
				player.lifeRegen = 0;
				player.lifeRegenTime = 0;
				player.statDefense -= 10;
				player.endurance /= 1.3f;
				player.maxMinions /= 2;
				player.ammoBox = false;
			}

			if (HellGuardCurse) {
				player.bleed = true;
				player.statDefense /= 2;
				player.endurance /= 2f;
				player.onFire2 = true;
				player.lifeRegen = 0;
				player.allDamage /= 1.1f;
				player.lifeRegenTime = 0;
			}
		}

		public override void PostUpdateEquips()
		{
			if (GodImitator)
				if (player.whoAmI == Main.myPlayer &&
				    player.ownedProjectileCounts[ModContent.ProjectileType<Imitator>()] == 0) {
					const float radius = (float) Math.PI * 2f / 5f;
					for (int i = 0; i < 5; i++)
						Projectile.NewProjectile(player.Center + new Vector2(60f, 0f).RotatedBy(radius * i),
							Vector2.Zero, ModContent.ProjectileType<Imitator>(), 150, 10f, player.whoAmI, 0f,
							radius * i);
				}
		}
	}
}