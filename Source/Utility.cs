using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    internal static class Utility
    {
        public static bool IsTickInterval(int tick)
        {
            if (tick > 0) 
            {
                return Find.TickManager.TicksGame % tick == 0;
            }
            return false;
        }
    }
}
