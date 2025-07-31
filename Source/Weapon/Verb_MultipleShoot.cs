using Verse;

namespace Astrologer
{
    public class VerbProperties_MultipleShoot : VerbProperties
    {
        public int projectilesCount = 1;
    }

    //霰弹说是 凑合用
    public class Verb_MultipleShoot : Verb_Shoot
    {
        protected VerbProperties_MultipleShoot Props => (VerbProperties_MultipleShoot)this.verbProps;
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
