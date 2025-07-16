using AK_DLL;

namespace Astrologer
{
    public class ApparelAstro : Apparel_Operator
    {
        public override string WornGraphicPath_MultiFoam
        {
            get
            {
                if (!Wearer.Drafted)
                {
                    return WornGraphicPath;
                }
                return (def.graphicData as GraphicData_MultiFoam).GetWornGraphicPathWithIndex(graphicIndex, this);
            }
        }
    }
}
