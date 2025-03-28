using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Astrologer.Insight
{
    public class JobDriver_InsightInteract : JobDriver
    {
        protected TargetIndex indexBuilding = TargetIndex.A;
        ThingWithComps ThingBuilding => job.GetTarget(indexBuilding).Thing as ThingWithComps;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(ThingBuilding, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(indexBuilding);
            yield return Toils_Goto.GotoCell(indexBuilding, PathEndMode.InteractionCell);

            Toil t = new();
            t.initAction = delegate
            {
                TC_InsightInteractable_Base interactable = ThingBuilding.TryGetComp<TC_InsightInteractable_Base>();
                interactable?.InsightInteract(this.pawn);
            };
            yield return t;
        }
    }
}
