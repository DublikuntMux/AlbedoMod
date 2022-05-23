using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Enemies
{
	public class MinionProjectile : ModProjectile
	{
		public override string Texture => "Albedo/Projectiles/Empty";
		
		public override void SetDefaults()
		{
			projectile.ranged = true;
			projectile.width = 4;
			projectile.height = 20;
			projectile.aiStyle = 1;
			aiType = 14;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.penetrate = 5;
			projectile.timeLeft = 400;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.scale = 0.7f;
			projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.position, 0.9f, 0.1f, 0.1f);
			Lighting.Brightness(1, 1);
			int dust = Dust.NewDust(projectile.Center, 1, 1, 6, 0f, 0f, 0, default, 0.6f);
			Main.dust[dust].velocity *= 0.3f;
			Main.dust[dust].scale = Main.rand.Next(100, 135) * 0.013f;
			Main.dust[dust].noGravity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) =>
			target.AddBuff(BuffID.OnFire, 120);

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Dig, (int) projectile.position.X, (int) projectile.position.Y, 21, 0.5f, 0.8f);
			for (int i = 0; i < 6; i++) Dust.NewDust(projectile.position, projectile.width, projectile.height, 7);
		}
	}
}