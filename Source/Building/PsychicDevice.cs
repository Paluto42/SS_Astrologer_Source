using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class PsychicDevice : Building
    {
        public readonly static float rotationSpeed = 15f;

        public readonly static Vector2 size = new(2f, 2f);

        private CompPowerTrader compPowerTrader;

        CompPowerTrader CompPower 
        {
            get
            {
                compPowerTrader ??= GetComp<CompPowerTrader>();
                return compPowerTrader;
            }
        }

        private Effecter effecter;
        private static Material EffectsMat => AstroMatPool.PsychicDevice_Particle;
        private static float EffectAngle => Time.time * rotationSpeed;
        private static Quaternion EffectRotation => Quaternion.Euler(0, EffectAngle, 0);
        private Vector3 EffectDrawPos => DrawPos + new Vector3(0, 0.1f, 0.1f);

        public bool PowerOn => CompPower != null && CompPower.PowerOn;

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            if (!PowerOn) return;

            Matrix4x4 matrix = default;
            Vector3 s = new(size.x, 1f, size.y);
            matrix.SetTRS(EffectDrawPos, EffectRotation, s);
            Graphics.DrawMesh(MeshPool.plane20, matrix, EffectsMat, 0);
        }

        protected override void Tick()
        {
            base.Tick();
            if (!PowerOn) return;

            effecter ??= AstroDefOf.LOF_Effecter_PsychicPulse.SpawnAttached(this, base.Map);
            effecter?.EffectTick(this, this);
        }
    }
}
