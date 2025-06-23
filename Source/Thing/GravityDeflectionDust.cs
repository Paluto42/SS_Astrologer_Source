using UnityEngine;
using Verse;

namespace Astrologer
{
    public class GravityDeflectionDust : ThingWithComps
    {
        public const int lastTime = 2500;

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            return;
            //Comps_PostDraw();
        }

        protected override void Tick()
        {
            base.Tick();
        }
    }
}
