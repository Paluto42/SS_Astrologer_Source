using HarmonyLib;
using RimWorld;

//免疫受伤减速
namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(StaggerHandler), "Staggered", MethodType.Getter)]
    public class Patch_IgnoreStagger
    {
        [HarmonyPrefix]
        public static bool Prefix(StaggerHandler __instance, ref bool __result)
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