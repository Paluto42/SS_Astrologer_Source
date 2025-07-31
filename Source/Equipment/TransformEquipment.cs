using Astrologer.Insight;
using Verse;

namespace Astrologer
{
    public class TransformEquipment : ThingWithComps
    {
        private TC_FireMode compFiremodeInt;
        public TC_FireMode CompFiremode
        {
            get
            {
                if (compFiremodeInt != null) return compFiremodeInt;
                compFiremodeInt = GetComp<TC_FireMode>();
                return compFiremodeInt;
            }
        }

        private Graphic secGraphicInt;
        GraphicData TransformGraphic => CompFiremode?.Graphic;

        private Pawn_EquipmentTracker PawnEqTracker => ParentHolder as Pawn_EquipmentTracker;
        private Pawn Holder => PawnEqTracker?.pawn;

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
