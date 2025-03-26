using AK_TypeDef;
using Astrologer.Insight;
using Verse;

namespace Astrologer
{
    public class Gene_Astrologer : Gene
    {
        public override void PostAdd()
        {
            Ext_ForcedAstrologer ext = def.GetModExtension<Ext_ForcedAstrologer>();
            if (ext != null && ext.forcedBodyType != null)
            {
                pawn.story.bodyType = ext.forcedBodyType;
            }
            VAB_AstroTracker tracker = new(pawn, ext.astroAbility);
            pawn.AddDoc(new AstroDocument(pawn)
            {
                astroTracker = tracker,
            });
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
