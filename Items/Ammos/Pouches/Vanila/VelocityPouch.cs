using Albedo.Base;

namespace Albedo.Items.Ammos.Pouches.Vanila
{
    public class VelocityPouch : BasePouch
    {
        protected override int AmmunitionItem => 1302;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.shootSpeed = 28f;
        }
    }
}