using AKA_Ability;
using Astrologer.Insight;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Ability
{
    public class AKAbility_FireMode : AKAbility_Base
    {
        private ThingWithComps Weapon => CasterPawn?.equipment?.Primary;

        private TC_FireMode compFiremodeInt;
        public TC_FireMode CompFiremode
        {
            get
            {
                if (compFiremodeInt != null) return compFiremodeInt;
                compFiremodeInt = Weapon?.GetComp<TC_FireMode>();
                return compFiremodeInt;
            }
        }

        public AKAbility_FireMode(AbilityTracker tracker) : base(tracker)
        {
        }

        public AKAbility_FireMode(AKAbilityDef def, AbilityTracker tracker) : base(def, tracker)
        {
        }

        //不能删
        /*public override void Tick()
        {
            if (Weapon == null) return;
            FireModeComp?.CompTick();
        }*/

        //从武器comp上获取Gizmo
        public override IEnumerable<Command> GetGizmos()
        {
            if (!CasterPawn.Drafted && !def.displayGizmoUndraft) yield break;
            if (CompFiremode is null) yield break;
            foreach (Gizmo gizmo in CompFiremode.CompGetGizmosExtra())
            {
                yield return (Command)gizmo;
            }
        }

        protected override void InitializeGizmoInnate()
        {
        }
    }
}
