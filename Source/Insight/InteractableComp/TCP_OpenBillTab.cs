using RimWorld;
using Verse;

namespace Astrologer.Insight
{

    public class TCP_OpenBillTab : TCP_InsightInteractable_Base
    {
        public TCP_OpenBillTab()
        {
            compClass = typeof(TC_OpenBillTab);
        }
    }

    public class TC_OpenBillTab : TC_InsightInteractable_Base
    {
        protected Building_CraftingTable SelTable => (Building_CraftingTable)parent;
        public override void InsightInteract(Pawn pawn)
        {
            base.InsightInteract(pawn);
            SelTable.canProduceNow = true;
        }
    }
}
