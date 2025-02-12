using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Astrologer.MapBuilding
{
    public class StellarStove : Building
    {
        public bool canProduceNow = false;
        public CompPowerTrader CompPower => GetComp<CompPowerTrader>();
        public CompRefuelable CompRefuelable => GetComp<CompRefuelable>();

        public override void Tick()
        {
            base.Tick();
        }
        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn) 
        {
            if (CompPower != null && !CompPower.PowerOn)
            {
                yield return new FloatMenuOption("CannotUseNoPower".Translate(), null, MenuOptionPriority.Default, null, null, 0f, null, null, true, 0); 
                yield break;
            }
            if (selPawn.health.Dead || selPawn == null && selPawn.CanReach(this, PathEndMode.Touch, Danger.Deadly))
            {
                yield return new FloatMenuOption("Astrologer_PawnNull".Translate(), null); 
                yield break;
            }
            if (selPawn.GetComp<TC_Insights>() != null) 
            {
                if (CompRefuelable.Fuel < 0.1) 
                {
                    yield break;
                }
                yield return new FloatMenuOption("Astrologer_StartupStove".Translate(),
                delegate
                {

                });
            }
            yield return null;
        }
    }
}
