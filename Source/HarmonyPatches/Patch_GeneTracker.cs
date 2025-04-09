using HarmonyLib;
using RimWorld;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_GeneTracker), "Notify_GenesChanged")]
    public class Patch_GeneTracker
    {
        [HarmonyPostfix]
        public static void Postfix_Notify_GenesChanged(ref Pawn ___pawn, ref GeneDef addedOrRemovedGene)
        {
            if (addedOrRemovedGene == null) return;
            Ext_ForcedHeadType headType = addedOrRemovedGene.GetModExtension<Ext_ForcedHeadType>();
            /*Ext_ForcedAstrologer main = addedOrRemovedGene.GetModExtension<Ext_ForcedAstrologer>();
            if (main != null && main.forcedBodyType != null)
            {
                ___pawn.story.bodyType = PawnGenerator.GetBodyTypeFor(___pawn);
                ___pawn.Drawer.renderer.SetAllGraphicsDirty();
            }*/
            //改变瞳色的专属子基因
            if (headType != null && headType.forcedHeadType != null)
            {
                ___pawn.story.headType = headType.forcedHeadType;
                ___pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
        }
    }
}
