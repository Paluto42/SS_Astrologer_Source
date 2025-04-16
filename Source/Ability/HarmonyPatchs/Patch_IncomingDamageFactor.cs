using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(StatExtension), "GetStatValue")]
    public class Patch_IncomingDamageFactor
    {
        public const float k = 0.5f;
        public static bool Prefix(Thing thing, StatDef stat, bool applyPostProcess, int cacheStaleAfterTicks, ref float __result)
        {
            if (stat != StatDefOf.IncomingDamageFactor) return true;
            if (thing is not Pawn pawn) return true;

            if (!pawn.EffectInDuration(EffectIDs.incomingDmgFactor)) return true;

            float healthPercent = pawn.health.summaryHealth.SummaryHealthPercent;
            float statValue = stat.Worker.GetValue(thing, applyPostProcess, cacheStaleAfterTicks);

            float factor = Mathf.Pow(healthPercent, k);
            __result = statValue * factor;
            return false;
        }
    }
}
