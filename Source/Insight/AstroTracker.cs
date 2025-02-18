using AKA_Ability;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class AstroTracker : VAbility_AKATrackerContainer
    {
        public float insight = 0;

        public AstroTracker(Pawn pawn) : base(pawn)
        {
        }

        public AstroTracker(Pawn pawn, AbilityDef def)
           : base(pawn, def)
        {
        }
    }
}
