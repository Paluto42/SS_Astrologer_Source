using Verse;

namespace Astrologer.Insight
{
    public class Verb_InsightShoot : Verb_Shoot
    {
        //private VAB_AstroTracker CompInsightInt => CasterPawn?.TryGetAstroTracker();
        public TC_FireMode CompFireMode => EquipmentSource?.GetComp<TC_FireMode>();
        public bool ShouldConsumeInsight => CompFireMode.IsSecondaryVerbSelected;
        public bool ShouldCalulateBursts => CompFireMode.Props.verbProp.burstShotCount > 0;

        //连射武器改成倒数第二发结算了 懒得改
        protected override bool TryCastShot()
        {
            bool IF_SUCCESS_CAST = base.TryCastShot();
            if (IF_SUCCESS_CAST && CompFireMode != null)
            {
                PreApplyTryCastShot();
                CompFireMode.Notify_Launched();
            }
            return IF_SUCCESS_CAST;
        }

        protected virtual void PreApplyTryCastShot()
        {
            if (!ShouldConsumeInsight || !ShouldCalulateBursts) return;
            if (CompFireMode.burstStatus == BurstFireStatus.None || CompFireMode.burstStatus == BurstFireStatus.Completed)
            {
                CompFireMode.burstStatus = BurstFireStatus.Started;
                return;
            }
        }
    }
}
