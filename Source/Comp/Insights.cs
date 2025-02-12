using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    //洞察力
    public class TCP_Insights : CompProperties
    {
        //1000tick增加1
        public int interval = 1000;
        //最大洞察力上限
        public int maxInsights = 10;
        public TCP_Insights()
        {
            this.compClass = typeof(TC_Insights);
        }
    }

    public class TC_Insights : ThingComp 
    {
        public TCP_Insights Props => (TCP_Insights)props;
        public Pawn User => parent as Pawn;
        private int tick = 0;
        private int curInsightAmountInt = 0;
        public int CurInsights => curInsightAmountInt;
        public int MaxInsights => Props.maxInsights;
        public override void CompTick()
        {
            if (User == null || User.health.Dead) return;
            if (User.health.Dead) return;
            ++tick;
            if (tick >= Props.interval) 
            {
                tick = 0;
                if (curInsightAmountInt < Props.maxInsights) 
                {
                    curInsightAmountInt++;
                }
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            yield return new Gizmo_Insight
            {
                compInsights = this
            };
        }

    }
}
