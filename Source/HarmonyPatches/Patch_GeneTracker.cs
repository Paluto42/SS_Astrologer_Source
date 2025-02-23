using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_GeneTracker), "Notify_GenesChanged")]
    public static class Patch_GeneTracker
    {
        [HarmonyPostfix]
        public static void Postfix_Notify_GenesChanged(ref Pawn ___pawn, ref GeneDef addedOrRemovedGene)
        {
            Ext_AstrologerGene ext = addedOrRemovedGene.GetModExtension<Ext_AstrologerGene>();
            if (addedOrRemovedGene != null && ext != null && (ext.forcedBodyType != null))
            {
                ___pawn.story.bodyType = PawnGenerator.GetBodyTypeFor(___pawn);
                ___pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
        }
    }
}
