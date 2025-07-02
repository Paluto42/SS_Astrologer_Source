using RimWorld;
using Verse;

namespace Astrologer
{
    public class ActivatedPulsePack : Building
    {
        public const float minRange = 5f;
        public const float maxRange = 10;
        public const int duration = 64;
        public const int lastTime = 2500;
        public const float screenShakeFactor = 0.125f;
        public float Radius => Rand.Range(minRange, maxRange);

        #if ver16
        protected override void Tick()
        {
            if (Utility.IsTickInterval(duration))
            {
                DamageCloseThings();
            }
            if (Utility.IsTickInterval(lastTime))
            {
                this.Destroy();
            }
        }
        #endif
        #if !ver16
        public override void Tick()
        {
            if (Utility.IsTickInterval(duration))
            {
                DamageCloseThings();
            }
            if (Utility.IsTickInterval(lastTime))
            {
                this.Destroy();
            }
        }
        #endif
        private void DamageCloseThings()
        {
            GenExplosion.DoExplosion(Position, Map, Radius, DamageDefOf.EMP, null, damAmount: -1, armorPenetration: -1, explosionSound: SoundDefOf.Power_OnSmall, null, null, null, null, 0f, 0, postExplosionGasType: null, screenShakeFactor: screenShakeFactor);
            //GenExplosion.DoExplosion(Position, Map, Radius, AstroDefOf.LOF_Damage_EMP, null, damAmount: -1, armorPenetration: -1, explosionSound: SoundDefOf.Power_OnSmall, null, null, null, null, 0f, 0, postExplosionGasType: null, screenShakeFactor: screenShakeFactor);
        }
    }

}
