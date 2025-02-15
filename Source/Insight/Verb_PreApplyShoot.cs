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
        protected override bool TryCastShot()
        {
            //Log.Message("Invoked TryCastShot");
            PreApplyTryCastShot();
            base.TryCastShot();
            return true;
        }

        private void PreApplyTryCastShot() 
        {
            //Log.Message("Invoked PreApplyTryCastShot");
            if (CompInsightInt == null || CompFireMode == null) return;
            if (ShouldConsumeInsight)
            {
                //Log.Message("洞察力已经消耗");
                CompInsightInt.ConsumeInsight(ConsumeAmount);
            }
        }
    }
}
