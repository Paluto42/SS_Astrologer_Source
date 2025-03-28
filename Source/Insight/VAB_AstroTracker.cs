using AKA_Ability;
using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer.Insight
{
    //洞察力主容器
    public class VAB_AstroTracker : VAbility_AKATrackerContainer
    {
        public float insight = 0;

        public const float insightCapacity = 100;//上限

        public const float insightRegenPerTick = 2;//回复量
        public const float insightRegenTickInterval = 250;//回复间隔

        public const float insightRegenBonusNearStarGrass = 2; //靠近草时回复奖励

        public const float starGrassObserveDistance = 5;
        public VAB_AstroTracker(Pawn pawn) : base(pawn)
        {
        }

        public VAB_AstroTracker(Pawn pawn, AbilityDef def)
           : base(pawn, def)
        {
        }
        public void ConsumeInsight(int amount)
        {
            if (amount <= 0) return;
            if (insight < amount) return;
            insight -= amount;
        }

        public override void AbilityTick()
        {
            base.AbilityTick();//哎哟我的妈这行一直没加
            if (Utility.CrtTick % insightRegenTickInterval == 0)
            {
                insight += insightRegenPerTick;
                Map map = pawn.Map;
                if (map != null)
                {
                    foreach (Thing t in map.listerThings.ThingsOfDef(AstroDefOf.LOF_Plant_Starlightgrass))
                    {
                        if (PossibleToObserve(t))
                        {
                            insight += insightRegenBonusNearStarGrass;
                            break;
                        }
                    }
                }
                insight = Mathf.Clamp(insight, 0, insightCapacity);
            }
        }

        private bool PossibleToObserve(Thing thing)
        {
            if (thing.Position.InHorDistOf(pawn.Position, starGrassObserveDistance))
            {
                return GenSight.LineOfSight(thing.Position, pawn.Position, pawn.Map, skipFirstCell: true);
            }
            return false;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref insight, "insight", 0);
        }
    }
}
