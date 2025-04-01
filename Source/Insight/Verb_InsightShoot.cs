using Verse;

namespace Astrologer.Insight
{
    public class Verb_InsightShoot : Verb_Shoot
    {
        private VAB_AstroTracker CompInsightInt => CasterPawn?.TryGetAstroTracker();
        private TC_FireMode CompFireMode => EquipmentSource?.GetComp<TC_FireMode>();
        private bool ShouldConsumeInsight => CompFireMode.IsSecondaryVerbSelected;
        private int ConsumeAmount => CompFireMode.Props.consumeAmount;
        public bool ShouldCalulateTicks => CompFireMode.Props.consumeDuration > 0;

        protected override bool TryCastShot()
        {
            PreApplyTryCastShot();
            bool IF_SUCCESS_CAST = base.TryCastShot();
            return IF_SUCCESS_CAST;
        }

        protected virtual void PreApplyTryCastShot()
        {
            if (CompInsightInt == null || CompFireMode == null) return;
            if (ShouldCalulateTicks)
            {
                if (ShouldConsumeInsight && CompFireMode.tickStatus == FireTickStatus.None)
                {
                    CompFireMode.tickStatus = FireTickStatus.Started;
                    return;
                }
                if (CompFireMode.tickStatus == FireTickStatus.Completed)
                {
                    CompInsightInt.ConsumeInsight(ConsumeAmount);
                    CompFireMode.tickStatus = FireTickStatus.None;
                    return;
                }
            }
            else if (ShouldConsumeInsight)
            {
                CompInsightInt.ConsumeInsight(ConsumeAmount);
            }
        }
    }
}
