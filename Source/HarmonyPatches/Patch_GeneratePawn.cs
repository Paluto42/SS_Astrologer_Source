using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new Type[] { typeof(PawnGenerationRequest) })]
    public class Patch_GeneratePawn
    {
        [HarmonyPostfix]
        public static void Postfix(ref PawnGenerationRequest request, Pawn __result)
        {
            var astroDef = request.KindDef.GetModExtension<Ext_Astrologer>();
            if (astroDef?.operatorDef is { } operatorDef)
            {
                __result.story.headType = operatorDef.HeadTypeThisPawn;
                //__result.story.bodyType = operatorDef.BodyTypeThisPawn;

                __result.story.hairDef = operatorDef.HairThisPawn;
                __result.story.HairColor = operatorDef.HairColorThisPawn;

                __result.story.skinColorOverride = operatorDef.skinColor;
                __result.Drawer.renderer.SetAllGraphicsDirty();
            }
        } 
        /*[HarmonyPostfix]
        public static void Postfix(ref PawnGenerationRequest request, Pawn __result)
        {
            if (__result == null || __result.genes.HasAstroGene() == false) return;
            if (__result.story.hairDef == HairDefOf.Bald)
            {
                __result.story.hairDef = AstroDefOf.AstroHairDefs.RandomElement();
            }
            Gene astroGene = __result.genes.GetAstroGene();
            Ext_ForcedHairColor ext = astroGene.def.GetModExtension<Ext_ForcedHairColor>();
            if (ext != null && !ext.hairColorOverride.NullOrEmpty())
            {
                Color? color = ext.hairColorOverride.RandomElementByWeight((ColorOption pi) => pi.weight).only;
                __result.story.HairColor = color.Value;
            }
        }*/
    }
    [HarmonyPatch(typeof(PawnGenerator), "GetBodyTypeFor")]
    public class Patch_GetBodyTypeFor
    {
        [HarmonyPostfix]
        public static void Postfix_GetBodyTypeFor(ref Pawn pawn, ref BodyTypeDef __result)
        {
            if (pawn == null || !pawn.genes.HasAstroGene()) return;
            Gene astroGene = pawn.genes.GetAstroGene();
            Ext_ForcedAstrologer ext = astroGene.def.GetModExtension<Ext_ForcedAstrologer>();
            if (ext != null && astroGene.Active)
            {
                if (astroGene.def.forcedHeadTypes != null && !astroGene.def.forcedHeadTypes.Contains(pawn.story.headType))
                {
                    pawn.story.headType = astroGene.def.forcedHeadTypes[UnityEngine.Random.Range(0, astroGene.def.forcedHeadTypes.Count)];
                }
                else if (ext.forcedBodyType != null && pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                {
                    __result = ext.forcedBodyType;
                }
            }
        }
    }
}
