using Terraria;
using Terraria.ModLoader;

namespace Albedo.Projectiles.Boss.HellGuard
{
	public class CrystalBombShard : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_90";

		public override void SetStaticDefaults() => DisplayName.SetDefault("Crystal Shard");

		public override void SetDefaults()
		{
			projectile.CloneDefaults(90);
			aiType = 90;
			projectile.friendly = false;
			projectile.ranged = false;
			projectile.hostile = true;
			projectile.alpha = 0;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(30, 180);
			target.AddBuff(33, 180);
		}
	}
}