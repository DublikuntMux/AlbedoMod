using Terraria.ModLoader;

namespace Albedo.Base
{
	public abstract class BasBulletProjectile : ModProjectile
	{
		protected abstract int Penetrate { get; }

		public override void SetDefaults()
		{
			projectile.ranged = true;
			projectile.width = 4;
			projectile.height = 20;
			projectile.aiStyle = 1;
			aiType = 14;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = Penetrate;
			projectile.timeLeft = 400;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.scale = 0.7f;
			projectile.extraUpdates = 1;
		}
	}
}