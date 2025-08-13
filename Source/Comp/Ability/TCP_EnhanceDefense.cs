using AK_DLL;
using Verse;

namespace Astrologer
{
    public class TCP_EnhanceDefense : CompProperties
    {
        //public int duration = 0;
        public float armorRatingMultiplier = 1f;

        public TCP_EnhanceDefense()
        {
            compClass = typeof(TC_EnhanceDefense);
        }
    }

    public class TC_EnhanceDefense : ThingComp
    {
        public TCP_EnhanceDefense Props => props as TCP_EnhanceDefense;

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
