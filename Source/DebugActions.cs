using LudeonTK;
using System.Linq;
using Verse;

namespace Astrologer
{
    public static class DebugActions
    {
#if ver16
        [DebugAction("Astro Actions", "OffsetPostionX-5", false, false, false, false, false, 0, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
#endif
#if ver15
        [DebugAction("Astro Actions", "OffsetPostionX-5", false, false, false, false, 0, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
#endif

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
