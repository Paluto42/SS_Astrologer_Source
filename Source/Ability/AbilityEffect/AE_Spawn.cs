using AKA_Ability;
using Verse;

namespace Astrologer.Ability
{
    public class AE_Spawn : AbilityEffectBase
    {
        public ThingDef thing;

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            if (thing != null)
            {
                GenSpawn.Spawn(thing, target.Cell, caster.CasterPawn.MapHeld);
            }
            return base.DoEffect(caster, target);
        }
    }
}
