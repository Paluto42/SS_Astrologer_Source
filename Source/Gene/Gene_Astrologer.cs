using AK_TypeDef;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class Gene_Astrologer : Gene
    {
        public override void PostAdd()
        {
            Ext_AstrologerGene ext = def.GetModExtension<Ext_AstrologerGene>();
            if (ext != null && (ext.forcedBodyType != null))
            {
                pawn.story.bodyType = ext.forcedBodyType;
            }
            pawn.AddDoc<AstroDocument>(new AstroDocument() { 
                parent = pawn,
                astroTracker = new VAB_AstroTracker(pawn, ext.astroAbility),
            });
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
