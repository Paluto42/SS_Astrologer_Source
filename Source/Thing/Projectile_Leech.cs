using AKA_Ability;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //命中敌人时，扣洞察并吸血
    public class Projectile_Leech : Projectile_Explosive
    {
        AKAbility_Auto ability = null;
        //参数直接硬编译的 懒了
        //这个是控制吸血是否生效的技能
        AKAbilityDef LinkedAbility => AstroDefOf.LOF_Ability_LeechAttack;
        public const float InsightCost = 1;
        public const float HealAmount = 10;
        //生效需要武器上面的技能开启
        AKAbility_Auto Ability
        {
            get
            {
                if (ability == null)
                {
                    TC_AKATracker comp = CasterPawn?.equipment?.Primary?.TryGetComp<TC_AKATracker>();
                    ability = comp?.tracker.TryGetAbility(LinkedAbility) as AKAbility_Auto;
                }
                return ability;
            }
        }

        Pawn CasterPawn => Launcher as Pawn;

        /*public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
        }*/

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);
            if (Ability == null) return;
            if (Ability.AutoCast)
            {
                CasterPawn.TryOffsetInsight(InsightCost);
            }
        }

        /*protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);
            AbilityEffect_Heal.Heal(CasterPawn, healAmount);
            Log.Message("AbilityEffect_Heal");
        }*/
    }
}
