using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    //一次只能合成一种东西
    public class Building_CraftingTable : Building_WorkTable
    {
        public bool canProduceNow = false;

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
        {
            return base.GetFloatMenuOptions(selPawn);
        }

        public virtual void Notify_BillAdded()
        {
            canProduceNow = false;
        }

        public override void Notify_BillDeleted(Bill bill)
        {
            if (bill is not Bill_Production billpro) return;
            if (billpro.repeatCount > 0)
            {
                canProduceNow = true;
            }
        }
    }
}
