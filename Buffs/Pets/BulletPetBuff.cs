using Albedo.Global;
using Albedo.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Albedo.Buffs.Pets
{
    public class BulletPetBuff : ModBuff
    {
        public override void SetDefaults() {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<AlbedoPlayer>().BulletPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<BulletPet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
                Projectile.NewProjectile(player.position.X + player.width / 2,
                    player.position.Y + player.height / 2, 0f, 0f,
                    ModContent.ProjectileType<BulletPet>(), 0,
                    0f, player.whoAmI);
            }
        }
    }
}
