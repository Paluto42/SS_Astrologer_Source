using AKA_Ability;
using Verse;

namespace Astrologer.Ability
{
    public class HCP_IncreastHarvest : HediffCompProperties
    {
        public HCP_IncreastHarvest()
        {
            compClass = typeof(HC_IncreastHarvest);
        }
    }

    public class HC_IncreastHarvest : HediffComp
    {
        public HCP_Regrow Props => (HCP_Regrow)props;

        public override string CompLabelInBracketsExtra
        {
            get
            {
                return $"\n剩余:{parent.Severity * 10:0.0}环时";
            }
        }
    }
}
