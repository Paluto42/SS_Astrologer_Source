using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.HarmonyPatchs
{
    [HarmonyPatch(typeof(Pawn), "PostApplyDamage")]
    public class Patch_ForcePartDestruct
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn __instance, DamageInfo dinfo, float totalDamageDealt)
        {
            if (dinfo.Weapon != AstroDefOf.LOF_AMSR) return;
            if (dinfo.Def != AstroDefOf.LOF_Cast_ForcePartDestruct) return;
            Log.Message("ASAT-112 \"船边星云\"" + "一发摧毁!");
            BodyPartRecord originalPart = dinfo.HitPart;
            IEnumerable<BodyPartRecord> notMissingParts = __instance.health.hediffSet.GetNotMissingParts();
            IEnumerable<BodyPartRecord> targetPart = notMissingParts.Where((BodyPartRecord record) => record.def == originalPart.def);
            Hediff_MissingPart hediff_MissingPart = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, __instance);
            hediff_MissingPart.Part = originalPart;
            if (targetPart == null || targetPart.Count() == 0)
            {
                //如果这发已经摧毁了部件，就挑剩下的非关键部位摧毁
                hediff_MissingPart.Part = notMissingParts.Where((BodyPartRecord record) => record.IsCorePart == false).RandomElement();
            }
            if (hediff_MissingPart.Part != null)
            {
                __instance.health.AddHediff(hediff_MissingPart);
            }
        }
    }
}
