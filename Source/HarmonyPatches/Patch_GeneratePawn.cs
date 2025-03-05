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
    //为啥PawnHairColors没用
    /*[HarmonyPatch(typeof(PawnHairColors), "RandomHairColor", new Type[] { typeof(Pawn), typeof(Color), typeof(int) })]
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
            }
        }
    }*/
    //应该写在Transpiler内部Calls相关方法的时候修改，太麻烦没必要
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new Type[] { typeof(PawnGenerationRequest) })]
    public class Patch_GeneratePawn
    {
        [HarmonyPostfix]
        public static void Postfix(ref PawnGenerationRequest request, Pawn __result)
        {
            if (__result == null || __result.genes.HasAstroGene() == false) return;
            Gene astroGene = __result.genes.GetAstroGene();
            Ext_ForcedHairColor ext = astroGene.def.GetModExtension<Ext_ForcedHairColor>();
            if (ext != null && !ext.hairColorOverride.NullOrEmpty())
            {
                Color? color = ext.hairColorOverride.RandomElementByWeight((ColorOption pi) => pi.weight).only;
                __result.story.HairColor = color.Value;
            }
        }
    }
}
