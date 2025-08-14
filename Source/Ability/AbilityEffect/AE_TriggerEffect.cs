using AKA_Ability;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Ability
{
    public class AE_TriggerEffect : AbilityEffectBase
    {
        public int duration = 0;

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            List<Apparel> armors = caster.CasterPawn.apparel.WornApparel.FindAll(ap => ap.HasComp<TC_StatEffecter>());

            foreach (var item in armors)
            {
                TC_StatEffecter compEnhance = item.GetComp<TC_StatEffecter>();
                compEnhance?.DoEffect(duration);
            }
            return true;
        }
    }
}
