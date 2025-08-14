using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    public class TCP_StatEffecter : CompProperties
    {
        public List<StatModifier> statFactors;

        public TCP_StatEffecter()
        {
            compClass = typeof(TC_StatEffecter);
        }
    }

    public class TC_StatEffecter : ThingComp
    {
        public TCP_StatEffecter Props => props as TCP_StatEffecter;

        private int tick = 0;
        public bool Effect => tick > 0;

        public void DoEffect(int duration)
        {
            tick = duration;
        }

        public void RemoveEffect() 
        {
            tick = 0;
        }

        public override float GetStatFactor(StatDef stat)
        {
            float num = 1f;
            if (!Effect) return num;
            num *= Props.statFactors.GetStatFactorFromList(stat);
            return num;
        }

        public override void CompTick()
        {
            base.CompTick();
            if (tick > 0)
            {
                tick--;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref tick, "tick", 0);
        }
    }
}
