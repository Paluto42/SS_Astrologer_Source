using HarmonyLib;
using RimWorld;
using System.Text;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(MassUtility), "Capacity")]
    public class Patch_MassCapacity
    {
        public const float addtionCapacity = 20;
        [HarmonyPostfix]
        public static float Postfix(float value, Pawn p, StringBuilder explanation)
        {
            if (value == 0f) return 0f;
            if (!p.RaceProps.Humanlike) return value;
            if (p.apparel == null) return value;
            if (p.apparel.WornApparel.Find(ap => ap.def.defName == "LOF_Apparel_MiningArmor") == null) return value;
            value += addtionCapacity;
            return value;
        }
    }
}
