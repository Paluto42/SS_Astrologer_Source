using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    //1.6新写的
    public class PawnRenderNode_Apparel : Verse.PawnRenderNode_Apparel
    {
        public PawnRenderNode_Apparel(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, Apparel apparel) : base(pawn, props, tree, apparel)
        {
        }

        public PawnRenderNode_Apparel(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, Apparel apparel, bool useHeadMesh) : base(pawn, props, tree, apparel, useHeadMesh)
        {
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
