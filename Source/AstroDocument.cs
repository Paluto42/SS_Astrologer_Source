using AK_DLL.Document;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class AstroDocument : DocumentBase
    {
        public AstroTracker astroTracker;

        public AstroDocument() : base() { }

        public AstroDocument(Thing parent) : base(parent)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref astroTracker, "tracker");
        }
    }
}
