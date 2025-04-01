using Verse;

namespace Astrologer
{
    public class TCP_DrafterGraphic : CompProperties
    {
        public GraphicData graphicData;

        public TCP_DrafterGraphic()
        {
            compClass = typeof(TC_DrafterGraphic);
        }
    }

    public class TC_DrafterGraphic : ThingComp
    {
        public TCP_DrafterGraphic Props => (TCP_DrafterGraphic)props;
        public GraphicData Graphic => Props.graphicData;
    }
}
