using Albedo.Global;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Pets
{
	public class BulletPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 12;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(209);
			aiType = 209;
			projectile.width = 31;
			projectile.height = 38;
		}

		public override bool PreAI()
		{
			Main.player[projectile.owner].truffle = false;
			return true;
		}

		public override void AI()
		{
			var player = Main.player[projectile.owner];
			var modPlayer = player.GetModPlayer<AlbedoPlayer>();
			if (player.dead) modPlayer.BulletPet = false;
			if (modPlayer.BulletPet) projectile.timeLeft = 2;
		}
	}
}