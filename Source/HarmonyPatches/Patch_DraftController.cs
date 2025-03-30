using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_DraftController), "Drafted", MethodType.Setter)]
    public class Patch_DraftController
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn_DraftController __instance)
        {
            /*List<ApparelAstro> apparelOp = __instance.pawn.apparel.WornApparel.OfType<ApparelAstro>().ToList();
            if (apparelOp.NullOrEmpty()) return;*/
            __instance.pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
