using RimWorld;
using Verse;

namespace Astrologer.Insight
{
    public class TCP_UpgradeInsight : CompProperties
    {
        public float extraCapacity = 50;

        public TCP_UpgradeInsight()
        {
            compClass = typeof(TC_UpgradeInsight);
        }
    }

    public class TC_UpgradeInsight : ThingComp 
    {
        public TCP_UpgradeInsight Props => (TCP_UpgradeInsight)props;

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            VAB_AstroTracker Tracker = pawn.TryGetAstroTracker();
            if (Tracker == null) return;
            Tracker.extraInsightCapacity += Props.extraCapacity;
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);
            VAB_AstroTracker Tracker = pawn.TryGetAstroTracker();
            if (Tracker == null) return;
            Tracker.extraInsightCapacity -= Props.extraCapacity;
        }
    }
}
