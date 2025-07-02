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

        #if ver16
        protected override void Tick()
        {
            base.Tick();
        }
        #endif
        #if !ver16
        public override void Tick()
        {
            base.Tick();
        }
        #endif
    }
}
