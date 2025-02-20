using Verse;
using AK_TypeDef;
using RimWorld;

namespace Astrologer
{
    internal static class Utility
    {
        public static int CrtTick => Find.TickManager.TicksGame;

        public static bool IsTickInterval(int tick)
        {
            if (tick > 0) 
            {
                return Find.TickManager.TicksGame % tick == 0;
            }
            return false;
        }

        public static AstroDocument TryGetAstroDoc(this Pawn p)
        {
            AstroDocument doc = p?.TryGetDoc<AstroDocument>();

            return doc;
        }

        public static AstroTracker TryGetAstroTracker(this Pawn p)
        {
            return TryGetAstroDoc(p)?.astroTracker;
        }

        public static void TryOffsetInsight(this Pawn p, float amt)
        {
            AstroTracker tracker = TryGetAstroTracker(p);
            if (tracker == null) return;

            tracker.insight += amt;
        }

        public static bool BuildingWorking(this ThingWithComps building)
        {
            if (building == null) return true;

            if (building.AllComps.NullOrEmpty()) return true;

            foreach (ThingComp comp in building.AllComps)
            {
                if (comp is CompBreakdownable compBreakdownable && compBreakdownable.BrokenDown) return false;
                else if (comp is CompFlickable compFlickable && !compFlickable.SwitchIsOn) return false;
                else if (comp is CompPowerTrader compPower && !compPower.PowerOn) return false;
                else if (comp is CompRefuelable compRefuelable && !compRefuelable.HasFuel) return false;
            }

            return true;
        }
    }
}
