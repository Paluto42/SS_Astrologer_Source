using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    public class PRN_Apparel : PawnRenderNode_Apparel
    {
        public PRN_Apparel(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, Apparel apparel) : base(pawn, props, tree, apparel)
        {
        }

        public PRN_Apparel(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, Apparel apparel, bool useHeadMesh) : base(pawn, props, tree, apparel, useHeadMesh)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            return base.GraphicFor(pawn);
        }

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            if (HasGraphic(tree.pawn))
            {
                yield return GraphicFor(pawn);
            }
        }
    }
}
