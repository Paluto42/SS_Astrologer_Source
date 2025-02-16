using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class TCP_HediffApparel : CompProperties
    {
        public HediffDef hediff;
        public List<BodyPartDef> partsToAffect;
        public TCP_HediffApparel()
        {
            compClass = typeof(TC_HediffApparel);
        }
    }

    public class TC_HediffApparel : ThingComp 
    {
        public TCP_HediffApparel Props => (TCP_HediffApparel)props;

        public override void Notify_Equipped(Pawn pawn)
        {
            if (Props.hediff == null) return;
            if (pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff) != null) return;
            if (Props.partsToAffect != null)
            {
                var source = pawn.health.hediffSet.GetNotMissingParts();
                var parts = source.Where(record => Props.partsToAffect.Contains(record.def));
                foreach (BodyPartRecord part in parts)
                {
                    Hediff newHediff = HediffMaker.MakeHediff(Props.hediff, pawn, part);
                    pawn.health.AddHediff(newHediff);
                }

            }
            else 
            {
                Hediff newHediff = HediffMaker.MakeHediff(Props.hediff, pawn, null);
                pawn.health.AddHediff(newHediff);
            }
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            Hediff hediffToRemove = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff);
            if (hediffToRemove != null) 
            {
                pawn.health.RemoveHediff(hediffToRemove);
            }
        }
    }
}
