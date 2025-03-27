using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class Thing_ActivatedPulsePack : Building
    {
        //public float radius = 5f;
        public float Radius => Rand.Range(5, 10);

        public override void Tick()
        {
            Log.Message("Tick");
            base.Tick();
            if (Utility.IsTickInterval(15))
            {
                DamageCloseThings();
            }
            if (Utility.IsTickInterval(2500))
            {
                this.Destroy();
            }
        }
        public override void TickRare()
        {
            Log.Message("TickRare");
            base.TickRare();
        }
        public override void TickLong()
        {
            Log.Message("TickLong");
            base.TickLong();
        }
        private void DamageCloseThings()
        {
            /*int num = GenRadial.NumCellsInRadius(radius);
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = base.Position + GenRadial.RadialPattern[i];
                if (intVec.InBounds(base.Map))
                {
                    Pawn firstPawn = intVec.GetFirstPawn(base.Map);
                    if (firstPawn == null || !firstPawn.Downed || !Rand.Bool)
                    {
                        float damageFactor = GenMath.LerpDouble(0f, 4.2f, 1f, 0.2f, intVec.DistanceTo(base.Position));
                        DoDamage(intVec, damageFactor);
                    }
                }
            }*/
            GenExplosion.DoExplosion(Position, Map, Radius, AstroDefOf.LOF_EMP, null, damAmount: -1, -1f, explosionSound: SoundDefOf.Power_OnSmall, null, null, null, null, 0f, 0, postExplosionGasType: null);
            Log.Message("DamageCloseThings");
        }
    }
}
