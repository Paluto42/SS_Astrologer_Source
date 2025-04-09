using RimWorld;
using Verse;

namespace Astrologer
{
    public class JoyGiver_InteractBuildingCell : JoyGiver_InteractBuildingInteractionCell
    {
        public const float newChance = 8f;
        public override float GetChance(Pawn pawn)
        {
            if (def.joyKind.defName == "Telescope" && pawn?.TryGetAstroDoc() != null)
            {
                return newChance;
            }
            return base.GetChance(pawn);
        }
    }
}
