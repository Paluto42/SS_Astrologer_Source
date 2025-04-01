using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer
{
    public class DamageWorker_ForcePartDestruct : DamageWorker_AddInjury
    {
        public const float lastTime = 4f; //持续几小时

        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {
            DamageResult result = base.Apply(dinfo, thing);
            if (result.hitThing is Pawn target)
            {
                if (target.Dead) goto ret;
                if (dinfo.Weapon != AstroDefOf.LOF_Weapon_AMSR || dinfo.Def != AstroDefOf.LOF_Cast_ForcePartDestruct) goto ret;

                IEnumerable<BodyPartRecord> notMissingParts = target.health.hediffSet.GetNotMissingParts();
                if (!notMissingParts.Any()) goto ret;
                IEnumerable<BodyPartRecord> availableParts = notMissingParts.Where((BodyPartRecord record) => record.def != BodyPartDefOf.Head);
                if (!availableParts.Any()) goto ret;

                BodyPartRecord finalTarget = availableParts.RandomElement();
                Hediff_MissingPart hediff_MissingPart = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, target);
                hediff_MissingPart.Part = finalTarget;
                if (hediff_MissingPart.Part != null)
                {
                    target.health.AddHediff(hediff_MissingPart);
                }
                HealthUtility.AdjustSeverity(target, AstroDefOf.LOF_Hediff_NebulaRay, lastTime * 0.1f);
            }
        ret: return result;
        }
    }
}
