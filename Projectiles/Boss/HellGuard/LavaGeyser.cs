using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
	public class LavaGeyser : ModProjectile
	{
		public override string Texture => "Albedo/Projectiles/Empty";


		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.hide = true;
			projectile.extraUpdates = 14;
		}

		public override bool CanDamage() => false;

		public override void AI()
		{
			var tileSafely = Framing.GetTileSafely(projectile.Center);
			if (projectile.ai[1] == 0.0) {
				projectile.position.Y -= 16f;
				if (!tileSafely.nactive() || !Main.tileSolid[tileSafely.type]) {
					projectile.ai[1] = 1f;
					projectile.netUpdate = true;
				}
			}
			else if (tileSafely.nactive() && Main.tileSolid[tileSafely.type] && tileSafely.type != 19 &&
			         tileSafely.type != 380) {
				if (projectile.timeLeft > 90)
					projectile.timeLeft = 90;
				projectile.extraUpdates = 0;
				projectile.position.Y -= 16f;
				int index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, -8f);
				var dust = Main.dust[index];
				dust.velocity *= 3f;
			}
			else {
				projectile.position.Y += 16f;
			}

			if (projectile.timeLeft > 120)
				return;
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit) => target.AddBuff(24, 300);

		public override void Kill(int timeLeft)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return;
			Projectile.NewProjectile(projectile.Center, Vector2.UnitY * -8f, 654, projectile.damage, 0.0f,
				Main.myPlayer);
		}
	}
}