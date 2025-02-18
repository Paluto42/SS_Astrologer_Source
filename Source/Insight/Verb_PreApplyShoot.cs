using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class Verb_PreApplyShoot : Verb_Shoot
    {
        //Caster = Pawn
        private TC_Insights CompInsightInt => CasterPawn?.GetComp<TC_Insights>();
        private TC_FireMode CompFireMode => EquipmentSource?.GetComp<TC_FireMode>();
        private bool ShouldConsumeInsight => CompFireMode.IsSecondaryVerbSelected;
        private int ConsumeAmount => CompFireMode.Props.consumeInsight;
        public bool ShouldCalulateTicks => CompFireMode.Props.consumeDuration > 0;

        private bool tickCompleted = false;
        protected override bool TryCastShot()
        {
            PreApplyTryCastShot();
            base.TryCastShot();
            return true;
        }

        private void PreApplyTryCastShot() 
        {
            Log.Message("Invoked PreApplyTryCastShot");
            if (CompInsightInt == null || CompFireMode == null) return;
            if (ShouldCalulateTicks) 
            {
                if (ShouldConsumeInsight && CompFireMode.tickStatus == FireTickStatus.None)
                {
                    CompFireMode.tickStatus = FireTickStatus.Started;
                    Log.Message("tickStatus = FireTickStatus.Started");
                    return;
                }
                if (ShouldConsumeInsight && CompFireMode.tickStatus == FireTickStatus.Completed)
                {
                    Log.Message("Tick完成 洞察力已经消耗");
                    CompFireMode.tickStatus = FireTickStatus.None;
                    CompInsightInt.ConsumeInsight(ConsumeAmount);
                    return;
                }
            }
            else if(ShouldConsumeInsight) 
            {
                Log.Message("洞察力已经消耗");
                CompInsightInt.ConsumeInsight(ConsumeAmount);
            }
        }
    }
}
