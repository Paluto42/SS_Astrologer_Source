using Verse;

namespace Astrologer
{
    public class VerbProperties_Shotgun : VerbProperties
    {
        public int projectilesCount = 1;
    }

    //霰弹说是 凑合用
    public class Verb_ShootShotgun : Verb_Shoot
    {
        protected VerbProperties_Shotgun Props => (VerbProperties_Shotgun)this.verbProps;
        protected override bool TryCastShot()
        {
            bool result = base.TryCastShot();
            if (result && Props.projectilesCount > 1)
            {
                for (int i = 1; i < Props.projectilesCount; i++)
                {
                    base.TryCastShot();
                }
            }
            return result;
        }
    }
}
