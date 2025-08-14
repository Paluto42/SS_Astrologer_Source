using AKA_Ability;
using Verse;

namespace Astrologer
{
    internal class DamageWorker_Bomb : DamageWorker_AddInjury
    {
        //参数直接硬编译的 懒了
        //这个是控制吸血是否生效的技能
        AKAbilityDef LinkedAbility => AstroDefOf.LOF_Ability_LeechAttack;

        //生效需要武器上面的技能开启
        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {
            DamageResult result = base.Apply(dinfo, thing);
            if (result.hitThing is Pawn && dinfo.Instigator is Pawn caster)
            {
                if (dinfo.Weapon != AstroDefOf.LOF_Weapon_ShipborneRailgun || dinfo.Def != AstroDefOf.LOF_LeechBomb) goto ret;

                TC_AKATracker comp = caster?.equipment?.Primary?.TryGetComp<TC_AKATracker>();
                if (comp?.tracker.TryGetAbility(LinkedAbility) is not AKAbility_Auto ability) goto ret;
                if (ability.AutoCast)
                {
                    AbilityEffect_Heal.Heal(caster, Projectile_Leech.HealAmount);
                }
            }
        ret: return result;
        }
    }
}
