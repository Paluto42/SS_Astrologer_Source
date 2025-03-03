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
    public class VAB_AstroTracker : VAbility_AKATrackerContainer
    {
        public float insight = 0;

        public VAB_AstroTracker(Pawn pawn) : base(pawn)
        {
        }

        public VAB_AstroTracker(Pawn pawn, AbilityDef def)
           : base(pawn, def)
        {
        }

        public override void AbilityTick()
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref insight, "insight", 0);
        }
    }
}
