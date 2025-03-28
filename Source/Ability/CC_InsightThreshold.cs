using AKA_Ability;
using AKA_Ability.CastConditioner;
using Astrologer.Insight;

namespace Astrologer.Ability
{
    public class CC_InsightThreshold : CastConditioner_Base
    {
        public bool above = true;
        public float amount = 1;

        public override bool Castable(AKAbility_Base instance)
        {
            VAB_AstroTracker tracker = instance.CasterPawn?.TryGetAstroTracker();
            if (tracker == null) return false;

            bool castable = tracker.insight >= amount;
            if (!above) castable = !castable;

            if (!castable && instance is AKAbility_Auto auto)
            {
                auto.AutoCast = false;
            }

            return castable;
        }
    }
}
