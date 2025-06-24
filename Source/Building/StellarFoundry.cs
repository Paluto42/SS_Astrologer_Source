using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class StellarFoundry : Building_WorkTable
    {
        public CompPowerTrader CompPower => GetComp<CompPowerTrader>();
        public CompRefuelable CompRefuelable => GetComp<CompRefuelable>();
        public bool PowerOn => CompPower != null && CompPower.PowerOn;

        private static readonly Material[] EffectMats = { AstroMatPool.StellarFoundry_A, AstroMatPool.StellarFoundry_B, AstroMatPool.StellarFoundry_C };

        private Material CurEffectMat;

        private int index = 0;

        private int frame = 0;

        public readonly static Vector2 size = new(2f, 2f);
        private Vector3 EffectDrawPos => DrawPos + new Vector3(0, 0.1f, 0.25f);

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            if (!PowerOn) return;

            frame++;
            if (frame >= 30)
            {
                CurEffectMat = EffectMats[index];
                index = (index + 1) % 3;
                frame = 0;
            }

            Matrix4x4 matrix = default;
            Vector3 s = new(size.x, 1f, size.y);
            Quaternion q = Quaternion.AngleAxis(0, Vector3.up);
            matrix.SetTRS(EffectDrawPos, q, s);
            CurEffectMat ??= AstroMatPool.StellarFoundry_A;
            Graphics.DrawMesh(MeshPool.plane20, matrix, CurEffectMat, 0);
        }
    }
}
