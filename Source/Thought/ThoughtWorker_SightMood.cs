using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer
{
    //至少要有2个stages
    public class ThoughtWorker_SightMood : ThoughtWorker
    {
        public int PS = 0; //PlayStation
        public int NS = 1; //Nintendo Switch

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            List<BodyPartRecord> parts = p.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Eye);
            if (parts.Any() == false) return ThoughtState.Inactive;

            HediffSet hediffSet = p.health.hediffSet;
            if (hediffSet.HasHediff(HediffDefOf.Blindness))
            {
                return ThoughtState.ActiveAtStage(NS);
            }
            foreach (BodyPartRecord item in parts)
            {
                //眼球植入物,缺眼,失明都会不开心
                if (hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(item) || hediffSet.PartIsMissing(item))
                {
                    return ThoughtState.ActiveAtStage(NS);
                }
            }
            return ThoughtState.ActiveAtStage(PS);
        }
    }
}
