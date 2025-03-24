using AKA_Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Ability
{
    public class AE_IncreaseHarvest : AbilityEffect_AddHediff
    {
        float lastTime = 24f;
        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            this.hediffDef = DefDatabase<HediffDef>.GetNamed("LOF_Hediff_IncreaseHarvest");
            this.severity = lastTime * 0.1f;

            Pawn target_Pawn = target.Pawn;

            HediffWithComps hediff = target_Pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef) as HediffWithComps;
            if (hediff == null)
            {
                HealthUtility.AdjustSeverity(target_Pawn, this.hediffDef, 0.01f);
            }
            return base.DoEffect(caster, target);
        }
    }
}
