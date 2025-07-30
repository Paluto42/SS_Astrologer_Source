using Astrologer.Insight;
using Verse;

namespace Astrologer
{
    public class TransformEquipment : ThingWithComps
    {
        private Graphic secGraphicInt;
        private Pawn_EquipmentTracker PawnEqTracker => ParentHolder as Pawn_EquipmentTracker;
        private Pawn Holder => PawnEqTracker?.pawn;

        TC_FireMode CompFiremode => GetComp<TC_FireMode>();
        GraphicData TransformGraphic => CompFiremode?.Graphic;

        public virtual bool CanTransform()
        {
            return CompFiremode.IsSecondaryVerbSelected;
        }

        public override Graphic Graphic
        {
            get
            {
                if (TransformGraphic == null || Holder == null || !CanTransform())
                {
                    return base.Graphic;
                }
                return secGraphicInt ??= TransformGraphic.Graphic;
            }
        }
    }
}
