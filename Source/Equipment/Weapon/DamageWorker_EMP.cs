using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class DamageWorker_EMP : DamageWorker
    {
        //无烟的EMP脉冲爆炸
        protected override void ExplosionVisualEffectCenter(Explosion explosion)
        {
            if (def.explosionCenterFleck != null)
            {
                FleckMaker.Static(explosion.Position.ToVector3Shifted(), explosion.Map, def.explosionCenterFleck);
            }
            else if (def.explosionCenterMote != null)
            {
                MoteMaker.MakeStaticMote(explosion.Position.ToVector3Shifted(), explosion.Map, def.explosionCenterMote);
            }

            def.explosionCenterEffecter?.Spawn(explosion.Position, explosion.Map, Vector3.zero);

            if (def.explosionInteriorMote == null && def.explosionInteriorFleck == null && def.explosionInteriorEffecter == null)
            {
                return;
            }

            int num = Mathf.RoundToInt((float)Math.PI * explosion.radius * explosion.radius / 6f * def.explosionInteriorCellCountMultiplier);
            for (int j = 0; j < num; j++)
            {
                Vector3 vector = Gen.RandomHorizontalVector(explosion.radius * def.explosionInteriorCellDistanceMultiplier);
                if (def.explosionInteriorEffecter != null)
                {
                    Vector3 vect = explosion.Position.ToVector3Shifted() + vector;
                    def.explosionInteriorEffecter.Spawn(explosion.Position, vect.ToIntVec3(), explosion.Map);
                }
                else if (def.explosionInteriorFleck != null)
                {
                    FleckMaker.ThrowExplosionInterior(explosion.Position.ToVector3Shifted() + vector, explosion.Map, def.explosionInteriorFleck);
                }
                else
                {
                    MoteMaker.ThrowExplosionInteriorMote(explosion.Position.ToVector3Shifted() + vector, explosion.Map, def.explosionInteriorMote);
                }
            }
        }
    }
}
