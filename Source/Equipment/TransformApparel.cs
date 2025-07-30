using AK_DLL;

namespace Astrologer
{
    public class TransformApparel : Apparel_Operator
    {
        public virtual bool CanTransform()
        {
            return Wearer.Drafted;
        }

        public override string WornGraphicPath_MultiFoam
        {
            get
            {
                if (!CanTransform()) return WornGraphicPath;
                return (def.graphicData as GraphicData_MultiFoam).GetWornGraphicPathWithIndex(graphicIndex, this);
            }
        }
    }
}
