using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

//免疫受伤减速
namespace Astrologer.HarmonyPatchs
{
    [HarmonyPatch(typeof(StaggerHandler), "Staggered", MethodType.Getter)]
    public class Patch_IgnoreStagger
    {
        [HarmonyPrefix]
        public static bool fix(StaggerHandler __instance, ref bool __result)
        {
            if (!__instance.parent.EffectInDuration(EffectIDs.ignoreStagger))
            {
                return HarmonyPrefixRet.keepOriginal;
            }
            __result = false;
            return HarmonyPrefixRet.skipOriginal;
        }
    }
}