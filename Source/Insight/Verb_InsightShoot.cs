using Verse;

namespace Astrologer.Insight
{
    public class Verb_InsightShoot : Verb_Shoot
    {
        //Caster = Pawn
        private VAB_AstroTracker CompInsightInt => CasterPawn?.TryGetAstroTracker();
        private TC_newFireMode CompFireMode => EquipmentSource?.GetComp<TC_newFireMode>();
        private bool ShouldConsumeInsight => CompFireMode.IsSecondaryVerbSelected;
        private int ConsumeAmount => CompFireMode.Props.consumeAmount;
        public bool ShouldCalulateTicks => CompFireMode.Props.consumeDuration > 0;
        protected override bool TryCastShot()
        {
            PreApplyTryCastShot();
            base.TryCastShot();
            return true;
        }

        private void PreApplyTryCastShot()
        {
            //Log.Message("Invoked PreApplyTryCastShot");
            if (CompInsightInt == null || CompFireMode == null) return;
            if (ShouldCalulateTicks)
            {
                if (ShouldConsumeInsight && CompFireMode.tickStatus == FireTickStatus.None)
                {
                    CompFireMode.tickStatus = FireTickStatus.Started;
                    return;
                }
                if (ShouldConsumeInsight && CompFireMode.tickStatus == FireTickStatus.Completed)
                {
                    CompFireMode.tickStatus = FireTickStatus.None;
                    CompInsightInt.ConsumeInsight(ConsumeAmount);
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
