using AKA_Ability;
using Verse;

namespace Astrologer.Ability
{
    public class AE_UnstableCatalyst : AbilityEffectBase
    {
        public int duration = 2500;

        readonly string effect = EffectIDs.halfIgnoreDmg;

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            AstroDocument doc = caster.CasterPawn.TryGetAstroDoc();
            if (doc.EffectValid(effect)) return false;
            doc.EffectRefresh(effect, duration);
            return base.DoEffect(caster, target);
        }
    }
}
