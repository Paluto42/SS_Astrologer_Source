using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Insight
{
    public class TCP_InsightUsable : CompProperties_Usable
    {
        public int requiredInsight = 0;
        public TCP_InsightUsable()
        {
            compClass = typeof(TC_InsightUsable);
        }
    }

    public class TC_InsightUsable : CompUsable
    {
        public TCP_InsightUsable Prop => (TCP_InsightUsable)props;

        private Building_CraftingTable craftTable;
        protected Building_CraftingTable SelTable
        {
            get
            {

                if (craftTable == null && parent is Building_CraftingTable table)
                {
                    craftTable = table;
                }
                return craftTable;
            }
        }

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn myPawn)
        {
            foreach (var option in base.CompFloatMenuOptions(myPawn))
            {
                if (!option.Disabled && SelTable != null && SelTable.canProduceNow) continue;
                yield return option;
            }
        }

        public override AcceptanceReport CanBeUsedBy(Pawn p, bool forced = false, bool ignoreReserveAndReachable = false)
        {
            AcceptanceReport acceptanceReport = base.CanBeUsedBy(p, forced, ignoreReserveAndReachable);
            if (acceptanceReport.Accepted)
            {
                VAB_AstroTracker doc = p.TryGetAstroTracker();
                if (doc == null)
                {
                    return "LOF_NeedInsight".Translate(); //没有洞察力
                }
                if (doc.insight < Prop.requiredInsight)
                {
                    return "LOF_InsufficientInsight".Translate(); //洞察力不足
                }
            }
            return acceptanceReport;
        }
    }
}
