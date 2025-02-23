using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(PawnGenerator), "GetBodyTypeFor")]
    public static class Patch_PawnGenerator
    {
        [HarmonyPostfix]
        public static void Postfix(ref Pawn pawn, ref BodyTypeDef __result)
        {
            if (pawn == null || !Utility.HasAstroGene(pawn.genes))
            {
                return;
            }
            List<Gene> endogenes = pawn.genes.Endogenes;
            foreach (Gene item in endogenes)
            {
                Ext_AstrologerGene ext = item.def.GetModExtension<Ext_AstrologerGene>();
                if (ext != null && item.Active)
                {
                    if (item.def.forcedHeadTypes != null && !item.def.forcedHeadTypes.Contains(pawn.story.headType))
                    {
                        pawn.story.headType = item.def.forcedHeadTypes[Random.Range(0, item.def.forcedHeadTypes.Count)];
                    }
                    else if (ext.forcedBodyType != null && pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                    {
                        __result = ext.forcedBodyType;
                    }
                }
            }
        }
    }
}
