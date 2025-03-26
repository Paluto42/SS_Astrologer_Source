using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_StoryTracker), "SkinColor", MethodType.Getter)]
    public class Patch_SkinColor
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn ___pawn, ref Color __result)
        {
            if (___pawn.genes.HasAstroGene() == false) return;
            Gene astroGene = ___pawn.genes.GetAstroGene();
            Ext_ForcedAstrologer ext = astroGene.def.GetModExtension<Ext_ForcedAstrologer>();
            if (ext != null && ext.skinColorOverride != null)
            {
                __result = ext.skinColorOverride.Value;
            }
        }
    }
}
