using AKA_Ability;
using Astrologer.Insight;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Ability
{
    public class AKAbility_FireMode : AKAbility_Base
    {
        private Pawn Caster => container.owner;
        private ThingWithComps Weapon => Caster?.equipment?.Primary;
        private TC_newFireMode FireModeComp => Weapon?.GetComp<TC_newFireMode>();
        public AKAbility_FireMode(AbilityTracker tracker) : base(tracker)
        {
        }

        public AKAbility_FireMode(AKAbilityDef def, AbilityTracker tracker) : base(def, tracker)
        {
        }
        public override void Tick()
        {
            if (Weapon == null) return;
            FireModeComp?.CompTick();
        }
        //从武器comp上获取Gizmo
        public override IEnumerable<Command> GetGizmos()
        {
            if (!CasterPawn.Drafted && !def.displayGizmoUndraft)
                yield break;
            if (Weapon == null || Weapon.AllComps.NullOrEmpty() || FireModeComp == null)
                yield break;
            foreach (Gizmo gizmo in FireModeComp.CompGetGizmosExtra())
            {
                yield return (Command)gizmo;
            }
            yield break;
        }

        protected override void InitializeGizmoInnate()
        {
        }
    }
}
