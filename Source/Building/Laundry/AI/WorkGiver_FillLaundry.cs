using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Astrologer
{
    //不能简化成一个 -- 一个物品可能同时满足多个桶的条件，而这里同时只能返回一个job。此外，也很难用一个jobdriver控制选哪个桶。
    //保留wg而不直接去物品右键floatmenu加吧 也许以后用得上呢
    public class WorkGiver_FillLaundryRepair : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(ThingRequestGroup.HaulableEver);

        protected static int SEARCH_RADIUS = 30;

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            //if (forced) Log.Message($"fill laund {this.GetType()} at {pawn.Name}， {forced} ");
            return !forced;
        }
        protected virtual Predicate<Thing> LaundryValidator => delegate (Thing t)   //是否是合格的对应种类洗衣机
        {
            TC_LaundryRepair comp = t.TryGetComp<TC_LaundryRepair>();

            if (comp == null) return false;
            return CompValidator(comp);
        };

        //通用的 判断是否是合格的工作中的桶
        protected virtual Predicate<TC_LaundryBase> CompValidator => delegate (TC_LaundryBase comp)
        {
            if (comp == null) return false;

            if (!comp.AnyVacancy) return false;

            return true;
        };

        protected virtual JobDef FillJob => AstroDefOf.LOF_Job_FillLaundry;

        protected bool ContentValidator(Thing t)
        {
            return t.def.useHitPoints && t.HitPoints < t.MaxHitPoints;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!forced || !ContentValidator(t))   //显然先判断一个物品是否合法再搜索洗衣机更快
            {
                //Log.Message($"for {t} : {!forced}, {!_ContentValidator(t)}");
                return null;
            }

            if (!pawn.CanReserve(t)) return null;

            List<Building> laundrys = pawn.Map.listerBuildings.AllBuildingsColonistOfDef(AstroDefOf.LOF_Thing_Laundy);
            Thing laundry = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, laundrys, PathEndMode.ClosestTouch, TraverseParms.For(pawn), SEARCH_RADIUS, LaundryValidator);

            if (laundry == null) return null;

            if (!pawn.CanReserve(laundry)) return null;

            Job job = JobMaker.MakeJob(FillJob, t, laundry);

            return job;
        }
    }
}