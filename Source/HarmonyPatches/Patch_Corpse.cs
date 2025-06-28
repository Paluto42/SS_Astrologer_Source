using HarmonyLib;
using RimWorld;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(Corpse), "RotStageChanged")]
    public class Patch_Corpse
    {
        [HarmonyPrefix]
        public static bool Prefix(Corpse __instance)
        {
            Pawn pawn = __instance.InnerPawn;
            if (pawn.TryGetAstroDoc() == null) return true;
            if (__instance.GetRotStage() is RotStage.Rotting or RotStage.Dessicated)
            {
                Map map = pawn.MapHeld;
                IntVec3 pos = pawn.PositionHeld;
                __instance.Destroy();
                Thing t = ThingMaker.MakeThing(AstroDefOf.LOF_Plant_MoonFlower);
                if (GenPlace.TryPlaceThing(t, pos, map, ThingPlaceMode.Direct) || GenPlace.TryPlaceThing(t, pos, map, ThingPlaceMode.Near))
                {
                }
            }
            return false;
        }
    }
}
