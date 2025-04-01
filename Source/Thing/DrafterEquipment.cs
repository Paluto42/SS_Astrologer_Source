using Verse;

namespace Astrologer
{
    //给武器用的
    public class DrafterEquipment : ThingWithComps
    {
        private Graphic secGraphicInt;
        Pawn Holder => EqTracker?.pawn;
        Pawn_EquipmentTracker EqTracker => (Pawn_EquipmentTracker)ParentHolder;
        TC_DrafterGraphic Drafter => GetComp<TC_DrafterGraphic>();
        GraphicData DrafterGraphic => Drafter?.Graphic;

        public override Graphic Graphic
        {
            get
            {
                if (DrafterGraphic != null && Holder != null && Holder.Drafted)
                {
                    secGraphicInt ??= DrafterGraphic.Graphic;
                    return secGraphicInt;
                }
                return base.Graphic;
            }
        }
    }
}
