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
        public static bool BodyGraphicForPrefix_Prefix(ref bool __result, PawnRenderNode_Body __instance, Pawn pawn)
        {
            __result = true;
            if (pawn.story.bodyType.HasModExtension<Ext_ForcedAstrologer>() == false) 
            {
                return false;
            }
            return true;
        }
    }
}
