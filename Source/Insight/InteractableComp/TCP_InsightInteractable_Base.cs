using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Insight
{
    public class TCP_InsightInteractable_Base : CompProperties
    {
        public float insightCost = 0;

        public TCP_InsightInteractable_Base()
        {
            compClass = typeof(TC_InsightInteractable_Base);
        }
    }

    public abstract class TC_InsightInteractable_Base : ThingComp, IInsightInteractable
    {
        TCP_InsightInteractable_Base Props => props as TCP_InsightInteractable_Base;

        float Radius => Props.affectRadius;

        public virtual void InsightInteract(Pawn pawn)
        {
            pawn.TryOffsetInsight(Props.insightCost);
        }
    }
}
