using AKA_Ability;
using Astrologer.Insight;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Ability
{
    public class AKAbility_FireMode : AKAbility_Base
    {
        private ThingWithComps Weapon => CasterPawn?.equipment?.Primary;
        private TC_FireMode FireModeComp => Weapon?.GetComp<TC_FireMode>();

        public AKAbility_FireMode(AbilityTracker tracker) : base(tracker)
        {
        }
        public AKAbility_FireMode(AKAbilityDef def, AbilityTracker tracker) : base(def, tracker)
        {
        }

        //不能删
        public override void Tick()
        {
            if (Weapon == null) return;
            FireModeComp?.CompTick();
        }

        //从武器comp上获取Gizmo
        public override IEnumerable<Command> GetGizmos()
        {
            if (!base.CasterPawn.Drafted && !def.displayGizmoUndraft)
                yield break;
            if (FireModeComp == null)
                yield break;
            foreach (Gizmo gizmo in FireModeComp.CompGetGizmosExtra())
            {
                yield return (Command)gizmo;
            }
        }

        protected override void InitializeGizmoInnate()
        {
        }
    }
}
