using AKA_Ability;
using RimWorld;
using Verse;

namespace Astrologer
{
    //命中敌人时，扣洞察并吸血
    public class Projectile_Leech : Bullet
    {
        AKAbility_Auto ability = null;

        //参数直接硬编译的 懒了
        //这个是控制吸血是否生效的技能
        const AKAbilityDef linkedAbility = default(AKAbilityDef);
        const float insightCost = 1;
        const float healAmount = 1;

        //生效需要武器上面的技能开启
        AKAbility_Auto Ability
        {
            get
            {
                if (ability == null)
                {
                    TC_AKATracker comp = CasterPawn.equipment.Primary?.TryGetComp<TC_AKATracker>();
                    ability = comp?.tracker.TryGetAbility(linkedAbility) as AKAbility_Auto;
                }
                return ability;
            }
        }

        Pawn CasterPawn => launcher as Pawn;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (Ability.AutoCast)
            {
                CasterPawn.TryOffsetInsight(insightCost);
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);
            AbilityEffect_Heal.Heal(CasterPawn, healAmount);
        }
    }
}
