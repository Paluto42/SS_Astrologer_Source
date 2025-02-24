using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.HarmonyPatches
{
    //朦胧暗月
    //持续消耗洞察力可以进入不稳定态，此时有一半的几率受击不造成伤害。
    [HarmonyPatch(typeof(Pawn), "PreApplyDamage")]
    public class Patch_IgnoreDmg
    {
        [HarmonyPrefix]
        public static bool prefix(Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            if (!__instance.EffectInDuration(EffectIDs.halfIgnoreDmg) || Rand.Range(0, 100) < 50)
            {
                absorbed = false;
                return HarmonyPrefixRet.keepOriginal;
            }
            absorbed = true;
            return HarmonyPrefixRet.skipOriginal;
        }
    }
}
