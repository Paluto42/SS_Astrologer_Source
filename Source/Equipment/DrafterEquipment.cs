using Verse;

namespace Astrologer
{
    //给武器用的
    /*public class DrafterEquipment : ThingWithComps
    {
        private Graphic secGraphicInt;
        Pawn_EquipmentTracker PawnEqTracker => (Pawn_EquipmentTracker)ParentHolder; //不能直接用哦
        Pawn Holder
        {
            get
            {
                if (ParentHolder == null || ParentHolder is not Pawn_EquipmentTracker) return null;
                return PawnEqTracker.pawn;
            }
        }
        TC_DrafterGraphic CompDrafter => GetComp<TC_DrafterGraphic>();
        GraphicData DrafterGraphic => CompDrafter?.Graphic;

        public override Graphic Graphic
        {
            get
            {
                if (DrafterGraphic == null || Holder == null || !Holder.Drafted)
                {
                    return base.Graphic;
                }
                secGraphicInt ??= DrafterGraphic.Graphic;
                return secGraphicInt;
            }
        }
    }*/
}
