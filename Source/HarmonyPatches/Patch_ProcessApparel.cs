using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(DynamicPawnRenderNodeSetup_Apparel), "GetDynamicNodes")]
    public class Patch_ProcessApparel
    {
        [HarmonyPostfix]
        public static IEnumerable<(PawnRenderNode node, PawnRenderNode parent)>
            Postfix(IEnumerable<(PawnRenderNode node, PawnRenderNode parent)> values, Pawn pawn, PawnRenderTree tree)
        {
            foreach (var value in values)
            {
                yield return value;
            }
            foreach (Apparel item in pawn.apparel.WornApparel)
            {
                if (item.def.IsWeapon)
                {
                    continue;
                }
                foreach (var result in ProcessApparelAgain(pawn, tree, item))
                {
                    if (result.node != null)
                    {
                        yield return result;
                    }
                }
            }
        }

        private static IEnumerable<(PawnRenderNode node, PawnRenderNode parent)> ProcessApparelAgain(Pawn pawn, PawnRenderTree tree, Apparel ap)
        {
            if (ap.def.apparel.HasDefinedGraphicProperties)
            {
                var renderNodeProperties = ap.def.apparel.RenderNodeProperties;
                if (renderNodeProperties.Count >= 1)
                {
                    for (int i = 1; i < renderNodeProperties.Count; i++)
                    {
                        var renderNodeProperty = renderNodeProperties[i];
                        if (tree.ShouldAddNodeToTree(renderNodeProperty))
                        {
                            yield return (node: (PawnRenderNode_Apparel)Activator.CreateInstance(renderNodeProperty.nodeClass, pawn, renderNodeProperty, tree, ap), parent: null);
                        }
                    }
                }
            }
        }

    }
}
