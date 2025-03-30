using AK_TypeDef;
using Verse;

namespace Astrologer
{
    public class TCP_AddHediff : CompProperties
    {
        public HediffDef hediff;
        public BodyPartDef partToAffect = null;
        public float severity = 1f;

        public TCP_AddHediff()
        {
            compClass = typeof(TC_AddHediff);
        }
    }

    public class TC_AddHediff : ThingComp
    {
        public TCP_AddHediff Props => (TCP_AddHediff)props;

        public override void Notify_Equipped(Pawn pawn)
        {
            if (pawn.health == null) return;
            GenericUtilities.AddHediff(pawn, Props.hediff, Props.partToAffect, null, Props.severity);
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
