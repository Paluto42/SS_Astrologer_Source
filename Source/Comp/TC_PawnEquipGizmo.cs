using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    public class TCP_PawnEquipGizmo : CompProperties
    {
        public TCP_PawnEquipGizmo()
        {
            compClass = typeof(TC_PawnEquipGizmo);
        }
    }

    public class TC_PawnEquipGizmo : ThingComp
    {
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            ThingWithComps thingWithComps = ((parent is Pawn pawn) ? pawn.equipment.Primary : null);
            if (thingWithComps == null || thingWithComps.AllComps.NullOrEmpty())
            {
                yield break;
            }
            foreach (ThingComp thingComp in thingWithComps.AllComps)
            {
                if (thingComp is not TC_FireMode firemode)
                {
                    continue;
                }
                foreach (Gizmo item in firemode.CompGetGizmosExtra())
                {
                    yield return item;
                }
                yield break;
            }
        }
    }
}
