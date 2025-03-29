using AKA_Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Ability
{
    public class AKAbility_Equip : AKAbility_Base
    {
        private ThingWithComps Weapon => CasterPawn?.equipment?.Primary;
        private TC_AKATracker Tracker => Weapon?.GetComp<TC_AKATracker>();

        public AKAbility_Equip(AbilityTracker tracker) : base(tracker)
        {
        }

        public AKAbility_Equip(AKAbilityDef def, AbilityTracker tracker) : base(def, tracker)
        {
        }

        public override void Tick()
        {
            Tracker?.CompTick();
        }

        public override IEnumerable<Command> GetGizmos()
        {
            if (!base.CasterPawn.Drafted && !def.displayGizmoUndraft)
                yield break;
            if (Tracker == null)
                yield break;
            foreach (Gizmo gizmo in Tracker.CompGetWeaponGizmosExtra())
            {
                yield return (Command)gizmo;
            }
        }

        protected override void InitializeGizmoInnate()
        {
        }
    }
}
