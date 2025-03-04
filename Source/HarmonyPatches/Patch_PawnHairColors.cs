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
    //为啥没用
    [HarmonyPatch(typeof(PawnHairColors), "RandomHairColor", new Type[] { typeof(Pawn), typeof(Color), typeof(int) })]
    public class Patch_PawnHairColors
    {
        [HarmonyPostfix]
        public static void Postfix_RandomHairColor(Pawn pawn, ref Color __result)
        {
            if (pawn.genes.HasAstroGene() == false) return;
            Gene astroGene = pawn.genes.GetAstroGene();
            Ext_ForcedHairColor ext = astroGene.def.GetModExtension<Ext_ForcedHairColor>();
            if (ext != null && !ext.hairColorOverride.NullOrEmpty())
            {
                Color? color = ext.hairColorOverride[UnityEngine.Random.Range(0, ext.hairColorOverride.Count)];
                __result = color.Value;
                Log.Message(pawn.Label + " 发色为: " + __result);
            }
        }
    }
}
