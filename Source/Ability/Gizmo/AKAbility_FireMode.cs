using AKA_Ability;
using Astrologer.Insight;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Ability
{
    //显示Gizmo的载体
    public class AKAbility_FireMode : AKAbility_Base
    {
        private TransformEquipment Weapon => CasterPawn?.equipment?.Primary as TransformEquipment;
        public TC_FireMode CompFiremode => Weapon?.CompFiremode;

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
