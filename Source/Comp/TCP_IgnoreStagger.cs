using RimWorld;
using Verse;

namespace Astrologer
{
    public class TCP_IgnoreStagger : CompProperties
    {
        public int duration = 6000;
        public TCP_IgnoreStagger()
        {
            compClass = typeof(TC_IgnoreStagger);
        }
    }

    public class TC_IgnoreStagger : ThingComp
    {
        private string effect = EffectIDs.ignoreStagger;
        TCP_IgnoreStagger Props => (TCP_IgnoreStagger)props;
        Apparel Parent => (Apparel)parent;
        Pawn Wearer => Parent?.Wearer;
        AstroDocument Doc => Wearer?.TryGetAstroDoc();

        public override void CompDrawWornExtras()
        {
            if (Wearer == null || Doc == null) return;
            if (Doc.EffectValid(effect)) return;
            Doc.EffectRefresh(effect, Props.duration);
        }
    }
}
