using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //心灵低语驳斥装置 
    public class PsychicDevice : Building
    {
        public readonly static float rotationSpeed = 15f;

        public readonly static Vector2 size = new(2f, 2f);
        public readonly static Vector3 scale = new(size.x, 1f, size.y);
        public CompPowerTrader CompPower => GetComp<CompPowerTrader>();
        public bool PowerOn => CompPower != null && CompPower.PowerOn;

        private Effecter effecter;
        private static Material EffectsMat => AstroMatPool.PsychicDevice_Particle;
        private static float EffectAngle => Time.time * rotationSpeed;
        private static Quaternion EffectRotation => Quaternion.Euler(0, EffectAngle, 0);
        private Vector3 EffectDrawPos => DrawPos + new Vector3(0, 0.1f, 0.1f);

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            if (!PowerOn) return;

            Graphics.DrawMesh(MeshPool.plane20, Matrix4x4.TRS(EffectDrawPos, EffectRotation, scale), EffectsMat, 0);
        }

#if ver16
        protected override void Tick()
        {
            base.Tick();
            if (!PowerOn) return;
            effecter ??= AstroDefOf.LOF_Effecter_PsychicPulse.SpawnAttached(this, base.Map);
            effecter?.EffectTick(this, this);
        }
#endif
#if ver15
        public override void Tick()
        {
            base.Tick();
            if (!PowerOn) return;
            effecter ??= AstroDefOf.LOF_Effecter_PsychicPulse.SpawnAttached(this, base.Map);
            effecter?.EffectTick(this, this);
        }
#endif
    }
}
