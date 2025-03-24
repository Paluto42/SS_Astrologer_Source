using AKA_Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Ability
{
    public class HCP_IncreastHarvest : HediffCompProperties
    {
        public HCP_IncreastHarvest()
        {
            this.compClass = typeof(HC_IncreastHarvest);
        }
    }
    public class HC_IncreastHarvest : HediffComp
    {
        public HCP_Regrow Props => (HCP_Regrow)props;
        public override string CompLabelInBracketsExtra
        {
            get
            {
                return $"\n剩余:{base.parent.Severity * 10:0.0}环时";
            }
        }
    }
}
