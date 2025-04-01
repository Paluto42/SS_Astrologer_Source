using AKA_Ability;
using RimWorld;
using Verse;

namespace Astrologer.Ability
{
    //刷个建筑
    public class AE_DeployBuilding : AbilityEffectBase
    {
        public ThingDef building; //要部署的建筑
        public ThingDef equip;  //使用后会摧毁装备 能用就行
        public ThingDef apparel;  //使用后会摧毁装备 能用就行

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            if (building != null)
            {
                Thing rest = ThingMaker.MakeThing(building);
                GenPlace.TryPlaceThing(rest, target.Cell, caster.CasterPawn.Map, ThingPlaceMode.Near);
            }
            if (equip != null)
            {
                ThingWithComps equipment = caster.CasterPawn.equipment.AllEquipmentListForReading.Find(delegate (ThingWithComps twc)
                {
                    return twc.def == equip;
                });
                caster.CasterPawn.equipment.DestroyEquipment(equipment);
            }
            if (apparel != null)
            {
                Apparel equipment = caster.CasterPawn.apparel.WornApparel.Find(a => a.def == apparel);
                if (equipment != null)
                {
                    caster.CasterPawn.apparel.TryDrop(equipment);
                    equipment?.Destroy();
                }
            }
            return base.DoEffect(caster, target);
        }
    }
}
