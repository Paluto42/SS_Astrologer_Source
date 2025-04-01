using Verse;

namespace Astrologer
{
    public class TCP_TimedRemove : CompProperties
    {
        public int lastTime;
        public TCP_TimedRemove()
        {
            compClass = typeof(TC_TimedRemove);
        }
    }

    public class TC_TimedRemove : ThingComp
    {
        TCP_TimedRemove Props => (TCP_TimedRemove)props;
        public override void CompTick()
        {
            if (Utility.IsTickInterval(Props.lastTime))
            {
                parent.Destroy();
            }
        }
    }
}
