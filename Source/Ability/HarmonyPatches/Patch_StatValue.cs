using HarmonyLib;
using RimWorld;
using Verse;

namespace Astrologer.Ability.HarmonyPatches
{
    [HarmonyPatch(typeof(StatExtension))]
    internal class Patch_StatValue
    {
        [HarmonyPostfix]
        [HarmonyPatch("GetStatValue")]
        public static float Postfix(float value, Thing thing, StatDef stat)
        {
            if (stat != StatDefOf.ArmorRating_Blunt && stat != StatDefOf.ArmorRating_Sharp) return value;
            if (thing.TryGetComp<TC_EnhanceDefense>(out var comp) && comp.Effect)
            {
                value *= comp.Props.armorRatingMultiplier;
            }
            return value;
        }
    }
}
