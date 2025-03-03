using LudeonTK;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public static class DebugActions
    {
        [DebugAction("Astro Actions", "OffsetPostionX-5", false, false, false, false, 0, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void OffsetPositionX()
        {
            var t = Find.Selector.SelectedPawns?.First();
            if (t != null)
            {
                t.Position = new IntVec3(t.Position.x + 5, t.Position.y, t.Position.z);
            }
        }
    }
}
