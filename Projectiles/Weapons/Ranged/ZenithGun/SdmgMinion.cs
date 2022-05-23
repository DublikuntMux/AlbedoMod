using System;
using Albedo.Buffs.Item;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Weapons.Ranged.ZenithGun
{
	public class SdmgMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 66;
			projectile.height = 32;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 0f;
			projectile.penetrate = -1;
		}

		public override bool? CanCutTiles() => false;

		public override bool MinionContactDamage() => false;

		public override void AI()
		{
			var val = Main.player[projectile.owner];
			if (val.dead || !val.active) val.ClearBuff(ModContent.BuffType<ZenithGunMinionBuff>());
			if (val.HasBuff(ModContent.BuffType<ZenithGunMinionBuff>())) projectile.timeLeft = 2;
			var center = val.Center;
			double num = (projectile.minionPos - 1) * -Math.PI / 9.0;
			center.X += 64f * (float) Math.Cos(num) - 32f;
			center.Y += 64f * (float) Math.Sin(num);
			center.Y -= 10f;
			float num2 = (center - projectile.Center).Length();
			if (Main.myPlayer == val.whoAmI && num2 > 2000f) {
				projectile.position = center;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			projectile.position += (center - projectile.position) * new Vector2(0.2f, 0.2f);
			float num3 = (Main.MouseWorld - projectile.position).ToRotation() % ((float) Math.PI * 2f);
			if (num3 < (float) Math.PI / 2f && num3 > -(float) Math.PI / 2f) {
				projectile.rotation = num3;
				projectile.spriteDirection = 1;
			}
			else {
				projectile.rotation = num3 + (float) Math.PI;
				projectile.spriteDirection = -1;
			}

			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
		}
	}
}