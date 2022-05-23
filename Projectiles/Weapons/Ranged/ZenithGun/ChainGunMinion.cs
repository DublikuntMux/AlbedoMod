namespace Albedo.Projectiles.Weapons.Ranged.ZenithGun
{
	public class ChainGunMinion : SdmgMinion
	{
		public sealed override void SetDefaults()
		{
			projectile.width = 52;
			projectile.height = 32;
			projectile.minionPos = 2;
		}
	}
}