using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class Thing_ActivatedPulsePack : Building
    {
        public float radius = 5f;
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
            GenExplosion.DoExplosion(Position, Map, radius, DamageDefOf.EMP, null, damAmount: -1, -1f, explosionSound: SoundDefOf.EnergyShield_AbsorbDamage, null, null, null, null, 1f, 1, postExplosionGasType: null);
            Log.Message("DamageCloseThings");
        }
    }
}
