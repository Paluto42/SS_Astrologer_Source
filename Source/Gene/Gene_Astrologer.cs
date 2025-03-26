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
            /*pawn.AddDoc(new AstroDocument(pawn)
            {
                astroTracker = new VAB_AstroTracker(pawn, ext.astroAbility),
            });*/
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
