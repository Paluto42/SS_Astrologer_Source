using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Astrologer
{
    public class JobDriver_WatchBuildingFixed : JobDriver_WatchBuilding
    {
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.EndOnDespawnedOrNull(TargetIndex.A);
            Toil watch;
            if (base.TargetC.HasThing && base.TargetC.Thing is Building_Bed)
            {
                this.KeepLyingDown(TargetIndex.C);
                yield return Toils_Bed.ClaimBedIfNonMedical(TargetIndex.C);
                yield return Toils_Bed.GotoBed(TargetIndex.C);
                watch = Toils_LayDown.LayDown(TargetIndex.C, hasBed: true, lookForOtherJobs: false);
                watch.AddFailCondition(() => !watch.actor.Awake());
            }
            else
            {
                yield return Toils_Goto.GotoCell(TargetIndex.B, PathEndMode.OnCell);
                watch = ToilMaker.MakeToil("MakeNewToils");
            }
            watch.AddPreTickAction(delegate
            {
                WatchTickAction();
            });
            watch.AddFinishAction(delegate
            {
                JoyUtility.TryGainRecRoomThought(pawn);
                if (job.def.defName == "UseTelescope" && pawn.TryGetAstroDoc() != null)
                {
                    pawn.needs.mood.thoughts.memories.TryGainMemory(AstroDefOf.LOF_Thought_UseTelescopeMood);
                }
            });
            watch.defaultCompleteMode = ToilCompleteMode.Delay;
            watch.defaultDuration = job.def.joyDuration;
            watch.handlingFacing = true;
            if (base.TargetA.Thing.def.building != null && base.TargetA.Thing.def.building.effectWatching != null)
            {
                watch.WithEffect(() => base.TargetA.Thing.def.building.effectWatching, EffectTargetGetter);
            }
            yield return watch;
            LocalTargetInfo EffectTargetGetter()
            {
                return base.TargetA.Thing.OccupiedRect().RandomCell + IntVec3.North.RotatedBy(base.TargetA.Thing.Rotation);
            }
        }
    }
}
