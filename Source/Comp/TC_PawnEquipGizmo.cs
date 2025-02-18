using System;
using System.Collections.Generic;
using System.IO;
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
        private Pawn Caster => parent as Pawn;
        private ThingWithComps Weapon => Caster?.equipment.Primary;

        private TC_FireMode FireModeComp => Weapon?.GetComp<TC_FireMode>();
        public override void CompTick()
        {
            FireModeComp?.CompTick();
        }
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Weapon == null || Weapon.AllComps.NullOrEmpty())
            {
                yield break;
            }
            if (FireModeComp != null)
            {
                foreach (Gizmo item in FireModeComp.CompGetGizmosExtra())
                {
                    yield return item;
                }
                yield break;
            }
        }
    }
}
