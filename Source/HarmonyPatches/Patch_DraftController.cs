using HarmonyLib;
using RimWorld;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_DraftController), "Drafted", MethodType.Setter)]
    public class Patch_DraftController
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn_DraftController __instance)
        {
            /*List<TransformApparel> apparelOp = __instance.pawn.apparel.WornApparel.OfType<TransformApparel>().ToList();
            if (apparelOp.NullOrEmpty()) return;*/
            __instance.pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
