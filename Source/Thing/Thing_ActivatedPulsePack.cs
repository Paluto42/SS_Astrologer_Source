using RimWorld;
using Verse;

namespace Astrologer
{
    public class Thing_ActivatedPulsePack : Building
    {
        public const float minRange = 5f;
        public const float maxRange = 10;
        public const int duration = 20;
        public const int lastTime = 2500;
        public float Radius => Rand.Range(minRange, maxRange);

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
        private void DamageCloseThings()
        {
            GenExplosion.DoExplosion(Position, Map, Radius, AstroDefOf.LOF_EMP, null, damAmount: -1, -1f, explosionSound: SoundDefOf.Power_OnSmall, null, null, null, null, 0f, 0, postExplosionGasType: null, screenShakeFactor: 0.125f);
        }
    }
}
