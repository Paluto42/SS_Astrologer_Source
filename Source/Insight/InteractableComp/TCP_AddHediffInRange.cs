using AK_DLL;
using AK_TypeDef;
using Verse;

namespace Astrologer.Insight
{
    public class TCP_AddHediffInRange : TCP_InsightInteractable_Base
    {
        public float radius = 8f;
        public HediffDef hediff;

        public int activeTickPerInteract = 1000;
        public TCP_AddHediffInRange()
        {
            compClass = typeof(TC_AddHediffInRange);
        }
    }

    public class TC_AddHediffInRange : TC_InsightInteractable_Base
    {
        TCP_AddHediffInRange Props => props as TCP_AddHediffInRange;

        int activeUntilTick = -1;  //当前tick小于这个 就周期性加hediff

        private Effecter effecter;

        public override void InsightInteract(Pawn pawn)
        {
            base.InsightInteract(pawn);
            activeUntilTick = Utility.CrtTick + Props.activeTickPerInteract;
        }

        public override void CompTick()
        {
            base.CompTick();
            Tick(1);
        }
        public override void CompTickRare()
        {
            base.CompTickRare();
            Tick(TimeToTickDirect.tickRare);
        }
        public override void CompTickLong()
        {
            base.CompTickLong();
            Tick(TimeToTickDirect.tickLong);
        }

        public virtual void Tick(int amt)
        {
            if (Utility.CrtTick > activeUntilTick) return;
            if (Utility.CrtTick % 250 == 0)
            {
                effecter ??= DefDatabase<EffecterDef>.GetNamed("BlastMechBandShockwave").SpawnAttached(parent, parent.Map);
                effecter?.Trigger(parent, parent);
            }
            foreach (IntVec3 c in GenRadial.RadialCellsAround(parent.Position, Props.radius, true))
            {
                foreach (Thing t in c.GetThingList(parent.Map))
                {
                    if (t is Pawn p && p.health != null)
                    {
                        GenericUtilities.AddHediff(p, Props.hediff, severity: 99);
                    }
                }
            }

        }
    }
}
