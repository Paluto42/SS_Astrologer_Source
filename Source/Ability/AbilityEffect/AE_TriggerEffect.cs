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
            List<Apparel> armors = caster.CasterPawn.apparel.WornApparel.FindAll(ap => ap.HasComp<TC_EnhanceDefense>());

            foreach (var item in armors)
            {
                TC_EnhanceDefense compEnhance = item.GetComp<TC_EnhanceDefense>();
                compEnhance?.DoEffect(duration);
            }
            return true;
        }
    }
}
