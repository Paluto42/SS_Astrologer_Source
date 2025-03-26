namespace Astrologer
{
    /*public class TCP_PawnEquipGizmo : CompProperties
    {
        public TCP_PawnEquipGizmo()
        {
            compClass = typeof(TC_PawnEquipGizmo);
        }
    }

    public class TC_PawnEquipGizmo : ThingComp
    {
        private Pawn Caster => parent as Pawn;
        private ThingWithComps Weapon => Caster?.equipment?.Primary;

        private TC_newFireMode FireModeComp => Weapon?.GetComp<TC_newFireMode>();

        public override void CompTick()
        {
            FireModeComp?.CompTick();
        }
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Caster?.equipment == null) 
                yield break;
            if (Weapon == null || Weapon.AllComps.NullOrEmpty()) 
                yield break;
            if (FireModeComp == null) 
                yield break;
            foreach (Gizmo item in FireModeComp.CompGetGizmosExtra())
            {
                yield return item;
            }
            yield break;
        }
    }*/
}
