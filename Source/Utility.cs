using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using AK_TypeDef;

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

        public static AstroDocument TryGetAstroDoc(this Pawn p)
        {
            AstroDocument doc = p?.TryGetDoc<AstroDocument>();

            return doc;
        }
    }
}
