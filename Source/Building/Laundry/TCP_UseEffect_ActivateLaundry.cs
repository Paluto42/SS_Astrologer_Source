using RimWorld;
using Verse;

namespace Astrologer.Laundry
{
    public class TCP_UseEffect_ActivateLaundry : CompProperties_UseEffect
    {
        public float insightCost = 1;
        public TCP_UseEffect_ActivateLaundry()
        {
            compClass = typeof(TC_UseEffect_ActivateLaundry);
        }
    }

    public class TC_UseEffect_ActivateLaundry : CompUseEffect
    {
        TCP_UseEffect_ActivateLaundry Prop => props as TCP_UseEffect_ActivateLaundry;
        float InsightCost => Prop.insightCost;
        public override AcceptanceReport CanBeUsedBy(Pawn p)
        {
            AstroDocument doc = p.TryGetAstroDoc();
            if (doc == null) return "不是占星师";
            if (doc.astroTracker.insight < InsightCost) return "洞察不足";
            return base.CanBeUsedBy(p);
        }

        public override void DoEffect(Pawn usedBy)
        {
            TC_LaundryRepair comp = parent.GetComp<TC_LaundryRepair>();
            if (!comp.working)
            {
                comp.working = true;
                usedBy.TryOffsetInsight(InsightCost);
            }
            base.DoEffect(usedBy);
        }
    }
}
