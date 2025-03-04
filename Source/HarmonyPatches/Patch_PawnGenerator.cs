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
    public class Patch_PawnGenerator
    {
        [HarmonyPostfix]
        public static void Postfix(ref Pawn pawn, ref BodyTypeDef __result)
        {
            if (pawn == null || !pawn.genes.HasAstroGene()) return;
            /*List<Gene> endogenes = pawn.genes.Endogenes;
            foreach (Gene gen in endogenes)
            {
                Ext_AstrologerGene ext = gen.def.GetModExtension<Ext_AstrologerGene>();
                if (ext != null && gen.Active)
                {
                    if (gen.def.forcedHeadTypes != null && !gen.def.forcedHeadTypes.Contains(pawn.story.headType))
                    {
                        pawn.story.headType = gen.def.forcedHeadTypes[Random.Range(0, gen.def.forcedHeadTypes.Count)];
                    }
                    else if (ext.forcedBodyType != null && pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                    {
                        __result = ext.forcedBodyType;
                    }
                }
            }*/
            Gene astroGene = pawn.genes.GetAstroGene();
            Ext_AstrologerGene ext = astroGene.def.GetModExtension<Ext_AstrologerGene>();
            if (ext != null && astroGene.Active)
            {
                if (astroGene.def.forcedHeadTypes != null && !astroGene.def.forcedHeadTypes.Contains(pawn.story.headType))
                {
                    pawn.story.headType = astroGene.def.forcedHeadTypes[Random.Range(0, astroGene.def.forcedHeadTypes.Count)];
                }
                else if (ext.forcedBodyType != null && pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                {
                    __result = ext.forcedBodyType;
                }
            }
        }
    }
}
