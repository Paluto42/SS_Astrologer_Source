using Astrologer.Insight;
using Verse;

namespace Astrologer
{
    public class TransformEquipment : ThingWithComps
    {
        #region 缓存
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

        private CompEquippable compEquippableInt;
        public CompEquippable EquipmentSource
        {
            get
            {
                compEquippableInt ??= GetComp<CompEquippable>();
                return compEquippableInt;
            }
        }
        #endregion

        private Pawn_EquipmentTracker PawnEqTracker => ParentHolder as Pawn_EquipmentTracker;
        private Pawn Holder => PawnEqTracker?.pawn;

        private Graphic secGraphicInt;
        GraphicData TransformGraphic => CompFiremode?.GraphicData;

        public virtual bool CanTransform()
        {
            return CompFiremode.IsSecondaryVerbSelected;
        }

        public override Graphic Graphic
        {
            get
            {
                if (Holder == null || TransformGraphic == null || !CanTransform())
                {
                    return base.Graphic;
                }
                return secGraphicInt ??= TransformGraphic.Graphic;
            }
        }
    }
}
