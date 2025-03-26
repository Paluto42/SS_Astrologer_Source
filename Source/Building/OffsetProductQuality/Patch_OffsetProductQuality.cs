using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(GenRecipe), "MakeRecipeProducts")]
    public class Patch_OffsetProductQuality
    {
        [HarmonyPostfix]
        public static IEnumerable<Thing> fix(IEnumerable<Thing> result, IBillGiver billGiver)
        {
            //bool shouldOffset = false;
            Ext_OffsetProductQuality ext = null;
            if (billGiver is Thing t)
            {
                ext = t.def.GetModExtension<Ext_OffsetProductQuality>();
                //shouldOffset = true;
            }
            foreach (Thing thing in result)
            {
                if (ext != null)
                {
                    CompQuality compQuality = thing.TryGetComp<CompQuality>();
                    compQuality?.SetQuality((QualityCategory)Mathf.Clamp((int)compQuality.Quality + ext.offsetAmout, (int)QualityCategory.Awful, (int)QualityCategory.Legendary), null);
                }
                yield return thing;
            }
        }
    }
}
