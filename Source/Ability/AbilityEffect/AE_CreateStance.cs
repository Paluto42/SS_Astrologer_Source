using AKA_Ability;
using RimWorld;
using Verse;

namespace Astrologer.Ability
{
    //创建立场(其实是范围性的Hediff)
    public class AE_CreateStance : AbilityEffect_AddHediff
    {
        public float radius = 10f;

        protected override bool DoEffect(AKAbility_Base caster, LocalTargetInfo target)
        {
            Map map = caster.CasterPawn.Map;
            IntVec3 center = target.Cell;
            if (map != null)
            {
                foreach (IntVec3 cell in GenRadial.RadialCellsAround(center, radius, true)) 
                {
                    if (!cell.InBounds(map)) continue;
                    foreach (Thing t in cell.GetThingList(map))
                    {
                        if (t is Pawn p && p.health != null && p.HostileTo(Faction.OfPlayer))
                        {
                            AddHediff(p, this.hediffDef, this.bodyPart, null, this.severity);
                        }
                    }
                }
            }
            return true;
        }
    }
}
