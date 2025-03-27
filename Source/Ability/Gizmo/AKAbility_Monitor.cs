using AKA_Ability;
using Astrologer.Insight;
using Verse;

namespace Astrologer.Ability
{
    //仅作为显示洞察力恢复条的容器
    public class AKAbility_Monitor : AKAbility_Base
    {
        private Pawn Caster => container.owner;
        private VAB_AstroTracker Tracker => Caster?.TryGetAstroTracker();

        public AKAbility_Monitor(AbilityTracker tracker) : base(tracker)
        {
        }

        public AKAbility_Monitor(AKAbilityDef def, AbilityTracker tracker) : base(def, tracker)
        {
        }

        protected override void InitializeGizmoInnate()
        {
            cachedGizmo = new Gizmo_InsightMonitor(Tracker)
            {
                defaultLabel = def.label,
                defaultDesc = def.description,
            };
        }
    }
}
