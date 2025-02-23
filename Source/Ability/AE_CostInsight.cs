using AKA_Ability;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class AE_CostInsight : AbilityEffectBase
    {
        public float amount = 1;

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            caster.CasterPawn.TryOffsetInsight(-amount);
            return base.DoEffect(caster, target);
        }
    }
}
