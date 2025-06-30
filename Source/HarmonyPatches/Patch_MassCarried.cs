using HarmonyLib;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn), "BodySize", MethodType.Getter)]
    public class Patch_MassCarried
    {
        public const float addtionCapacity = 0.6f;
        [HarmonyPrefix]
        public static bool Prefix(Pawn __instance, ref float __result)
        {
            if (!__instance.RaceProps.Humanlike) return true;
            if (__instance.apparel == null) return true;
            if (__instance.apparel.WornApparel.Find(ap => ap.def.defName == "LOF_Apparel_MiningArmor") != null) 
            {
                float origin = __instance.ageTracker.CurLifeStage.bodySizeFactor * __instance.RaceProps.baseBodySize;
                __result = origin + addtionCapacity;
                return false;
            }
            return true;
        }
    }
}
