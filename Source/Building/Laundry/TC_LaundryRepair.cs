using System;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    public class TC_LaundryRepair : TC_LaundryBase
    {
        //public override string SSR_ID => "LaundryRepair";

        protected override string GizmoActiveLabel => "SSR_开关生物编码洗衣机副桶";
        protected override string GizmoActiveDesc => "SSR_开关生物编码洗衣机副桶desc";

        List<Thing> pendingDropping = new();

        public override void CompleteWashing()
        {
            pendingDropping.Clear();
            foreach (Thing i in LinkedContainer.content)
            {
                if (i == null) continue;
                CompleteWashingSingle(i);
            }
            //working = false;

            if (!pendingDropping.NullOrEmpty())
            {
                foreach (Thing k in pendingDropping)
                {
                    //Thing res;
                    linkedContainer.content.TryDrop(k, parent.InteractionCell, parent.Map, ThingPlaceMode.Near, out _);
                }
            }

            working = !linkedContainer.content.NullOrEmpty();
        }
        protected override void CompleteWashingSingle(Thing t)
        {
            ThingWithComps j = t as ThingWithComps;
            if (j == null) goto LABEL_ShouldDrop;
            if (j.def.useHitPoints)
            {
                j.HitPoints = (int)Math.Min(j.MaxHitPoints, j.HitPoints + Prop.effect);
                if (j.HitPoints >= j.MaxHitPoints) goto LABEL_ShouldDrop;
            }
            else goto LABEL_ShouldDrop;
            return;

        LABEL_ShouldDrop:
            pendingDropping.Add(t);
        }

        protected override bool _ContentValidator(Thing t)
        {
            return t.def.useHitPoints && t.HitPoints < t.MaxHitPoints;
        }
    }
}