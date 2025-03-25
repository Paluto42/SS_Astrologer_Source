using AKA_Ability;
using Astrologer.Insight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FireModeComp?.CompTick();
        }
        public override IEnumerable<Command> GetGizmos()
        {
            if (!CasterPawn.Drafted && !def.displayGizmoUndraft) 
                yield break;
            if (Weapon == null || Weapon.AllComps.NullOrEmpty() || FireModeComp == null) 
                yield break;
            foreach (Gizmo item in FireModeComp.CompGetGizmosExtra())
            {
                yield return item as Command;
            }
            yield break;
        }

        protected override void InitializeGizmoInnate()
        {
        }
    }
}
