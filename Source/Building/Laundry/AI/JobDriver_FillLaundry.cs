using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Astrologer
{
    //好像jobdriver是可以用同一个的
    public class JobDriver_FillLaundryUnited : JobDriver
    {

        protected TargetIndex indexApparel = TargetIndex.A;
        protected TargetIndex indexLaundry = TargetIndex.B;

        protected ThingWithComps ThingLaundry => job.GetTarget(indexLaundry).Thing as ThingWithComps;
        protected Thing ThingApparel => job.GetTarget(indexApparel).Thing;

        //protected virtual Type CompType => typeof(TC_LaundryDeathStain);    //可能只需要重载这一个 因为是否是合理物品已经在workgiver判定，这里不太关心

        //public string CompID;

        protected TC_LaundryBase GetLaundryComp
        {
            get
            {
                return ThingLaundry.TryGetComp<TC_LaundryRepair>();
            }
        }
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if (pawn.Reserve(ThingLaundry, job, 1, -1, null, errorOnFailed))
            {
                return pawn.Reserve(ThingApparel, job, 1, -1, null, errorOnFailed);
            }
            return false;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            //Log.Message($"driver {this.GetHashCode()}, {this.CompID}");
            this.FailOnDestroyedOrNull(indexLaundry);
            this.FailOnDestroyedNullOrForbidden(indexApparel);

            //this.AddFailCondition();
            //this.AddFinishAction();

            yield return Toils_General.DoAtomic(delegate
            {
                job.count = ThingApparel.stackCount;
            });

            yield return Toils_Goto.GotoThing(indexApparel, PathEndMode.ClosestTouch).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            yield return Toils_Haul.StartCarryThing(indexApparel, putRemainderInQueue: false, subtractNumTakenFromJobCount: true);
            yield return Toils_Goto.GotoThing(indexLaundry, PathEndMode.ClosestTouch).FailOnSomeonePhysicallyInteracting(TargetIndex.A);

            Toil addThing = ToilMaker.MakeToil();
            TC_LaundryBase comp = GetLaundryComp;
            addThing.initAction = delegate ()
            {
                comp.LinkedContainer.TryAccept(ThingApparel);
            };
            addThing.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return addThing;
        }

        /*public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref CompID, "compID");
        }*/
    }
}
