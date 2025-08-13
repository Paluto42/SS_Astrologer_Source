using RimWorld;
using Verse;

namespace Astrologer
{
    public class TCP_EffectWorn : CompProperties
    {
        public string effect;
        public int duration = 6000;
        public TCP_EffectWorn()
        {
            compClass = typeof(TC_EffectWorn);
        }
    }

    public class TC_EffectWorn : ThingComp
    {
        //private string effect = EffectIDs.ignoreStagger;
        TCP_EffectWorn Props => (TCP_EffectWorn)props;
        Apparel Parent => (Apparel)parent;
        Pawn Wearer => Parent?.Wearer;
        AstroDocument Doc => Wearer?.TryGetAstroDoc();

        public override void CompDrawWornExtras()
        {
            if (Utility.CrtTick % Props.duration != 0) return; //定期刷新

            if (Wearer == null || Doc == null) return;
            if (Doc.EffectValid(Props.effect)) return;
            Doc.EffectRefresh(Props.effect, Props.duration);
        }
    }
}
