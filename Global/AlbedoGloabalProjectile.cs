using Terraria;
using Terraria.ModLoader;

namespace Albedo.Global
{
    public class AlbedoGloabalProjectile : GlobalProjectile
    {
        public int DeletionImmuneRank;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            switch (projectile.type)
            {
                case 384:
                case 386:
                    DeletionImmuneRank = 1;
                    break;
                case 642:
                    DeletionImmuneRank = 1;
                    break;
                case 460:
                case 461:
                case 632:
                case 633:
                    DeletionImmuneRank = 1;
                    break;
                case 656:
                    DeletionImmuneRank = 1;
                    break;
                case 447:
                case 455:
                case 537:
                case 657:
                case 658:
                    DeletionImmuneRank = 1;
                    break;
            }
        }
    }
}