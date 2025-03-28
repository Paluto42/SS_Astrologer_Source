using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer.HarmonyPatchs
{
    [HarmonyPatch(typeof(Pawn), "PostApplyDamage")]
    public class Patch_ForcePartDestruct
    {
        public const float lastTime = 4f;//

        [HarmonyPostfix]
        public static void Postfix(Pawn __instance, DamageInfo dinfo, float totalDamageDealt)
        {
            if (__instance.Dead) return;
            if (dinfo.Weapon != AstroDefOf.LOF_AMSR || dinfo.Def != AstroDefOf.LOF_Cast_ForcePartDestruct) return;

            IEnumerable<BodyPartRecord> notMissingParts = __instance.health.hediffSet.GetNotMissingParts();
            if (!notMissingParts.Any()) return;
            IEnumerable<BodyPartRecord> availableParts = notMissingParts.Where((BodyPartRecord record) => record.def != BodyPartDefOf.Head);
            if (!availableParts.Any()) return;

            BodyPartRecord finalTarget = availableParts.RandomElement();
            Hediff_MissingPart hediff_MissingPart = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, __instance);
            hediff_MissingPart.Part = finalTarget;
            if (hediff_MissingPart.Part != null)
            {
                __instance.health.AddHediff(hediff_MissingPart);
            }
            HealthUtility.AdjustSeverity(__instance, AstroDefOf.LOF_Hediff_NebulaRay, lastTime * 0.1f);
        }
    }
}
