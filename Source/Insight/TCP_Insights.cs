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
        public Pawn User => parent as Pawn;
        public TCP_Insights Props => (TCP_Insights)props;

        private int curInsightAmount = 0;
        //private int maxInsightAmount = 1;
        public int CurInsights => curInsightAmount;
        public int MaxInsights => Props.maxInsights;

        public void ConsumeInsight(int amount)
        {
            if (amount <= 0) return;
            if (curInsightAmount < amount) return;
            curInsightAmount -= amount;
            //Log.Message(User.Name + " 的洞察力消耗了： " + amount);
        }
        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }
        public override void CompTick()
        {
            if (User == null || User.health.Dead) return;
            if (Utility.IsTickInterval(Props.interval)) 
            {
                if (curInsightAmount < Props.maxInsights) 
                {
                    curInsightAmount++;
                }
            }
        }
        //放在Pawn身上可以直接调用
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            yield return new Gizmo_Insight
            {
                compInsights = this
            };
        }
    }
}
