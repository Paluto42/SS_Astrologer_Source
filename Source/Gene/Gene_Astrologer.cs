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
            //文档给的是个空VAB一定要替换掉!!!
            pawn.AddDoc<AstroDocument>(new AstroDocument()
            {
                parent = pawn,
                astroTracker = new VAB_AstroTracker(pawn, ext.astroAbility),
            });
            Log.Message("void::Gene_Astrologer.PostAdd >> AddDoc");
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
