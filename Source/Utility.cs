using AK_TypeDef;
using Astrologer.Insight;
using RimWorld;
using Verse;

namespace Astrologer
{
    public static class Utility
    {
        public static int CrtTick => Find.TickManager.TicksGame;

        public static bool HasAstroGene(this Pawn_GeneTracker genes)
        {
            if (genes == null) return false;
            if (genes.HasEndogene(AstroDefOf.LOF_Gene_Main)) return true;
            return false;
        }
        public static Gene GetAstroGene(this Pawn_GeneTracker genes)
        {
            if (genes == null) return null;
            return genes.GetGene(AstroDefOf.LOF_Gene_Main);
        }
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
            if (doc == null)
            {
                Log.WarningOnce(p.Label + "::TryGetAstroDoc failed", 114514);
            }
            return doc;
        }

        public static VAB_AstroTracker TryGetAstroTracker(this Pawn p)
        {
            VAB_AstroTracker tracker = TryGetAstroDoc(p)?.astroTracker;
            if (tracker == null)
            {
                Log.WarningOnce(p.Label + "::TryGetAstroTracker failed", 1919810);
            }
            return tracker;
        }

        //查询某个id的效果是否还在有效期
        public static bool EffectInDuration(this Pawn p, string effectID)
        {
            AstroDocument doc = p.TryGetAstroDoc();
            if (doc == null || !doc.EffectValid(effectID)) return false;
            return true;
        }

        public static void TryOffsetInsight(this Pawn p, float amt)
        {
            VAB_AstroTracker tracker = TryGetAstroTracker(p);
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
