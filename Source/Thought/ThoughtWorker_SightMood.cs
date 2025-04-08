using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer
{
    //至少要有2个stages
    public class ThoughtWorker_SightMood : ThoughtWorker
    {
        public int passiveStage = 0;
        public int negativeStage = 1;

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            List<BodyPartRecord> parts = p.RaceProps.body.GetPartsWithDef(BodyPartDefOf.Eye);
            if (parts.Any() == false) return ThoughtState.Inactive;

            HediffSet hediffSet = p.health.hediffSet;
            foreach (BodyPartRecord item in parts)
            {
                //眼球植入物,缺眼,失明都会不开心
                if (hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(item)
                    || hediffSet.PartIsMissing(item) || hediffSet.HasHediff(HediffDefOf.Blindness))
                {
                    return ThoughtState.ActiveAtStage(negativeStage);
                }
            }
            return ThoughtState.ActiveAtStage(passiveStage);
        }
    }
}
