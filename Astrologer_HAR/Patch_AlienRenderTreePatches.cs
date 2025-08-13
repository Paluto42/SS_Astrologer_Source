using AlienRace;
using Astrologer;
using HarmonyLib;
using System;
using Verse;

namespace Astrologer_HAR
{
    [HarmonyPatch(typeof(AlienRenderTreePatches), "BodyGraphicForPrefix")]
    [HarmonyPatch(new Type[] { typeof(PawnRenderNode_Body), typeof(Pawn), typeof(Graphic) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref })]
    public class Patch_BodyGraphicForPrefix
    {
        [HarmonyPrefix]
        public static bool BodyGraphicForPrefix_Prefix(ref bool __result, Pawn pawn)
        {
            __result = true;
            if (pawn.TryGetAstroDoc() != null)
            {
                return false;
            }
            return true;
        }
    }
}
