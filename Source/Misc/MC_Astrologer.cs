namespace Astrologer
{
    /*public class MC_Astrologer : MapComponent
    {
        public bool skyLanternRitualEffect = false;
        public MC_Astrologer(Map map) : base(map)
        {
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref skyLanternRitualEffect, "ritualEffect");
        }

        public override void MapComponentTick()
        {
            if (!skyLanternRitualEffect) return;
            if (Utility.CrtTick % 2500 == 0)
            {
                Log.Message("断点1");
                var corpses = map.listerThings.ThingsInGroup(ThingRequestGroup.Corpse).Cast<Corpse>();
                if (corpses.Count() == 0) return;
                Log.Message("断点2");
                foreach (Corpse co in corpses)
                {
                    if (co is not Corpse_Astrologer corpse) continue;
                    Pawn p = corpse.InnerPawn;
                    Log.Message(p.RaceProps.corpseDef.thingClass);
                    corpse.astroRitualEffect = true;
                }
                Log.Message("断点4");
                skyLanternRitualEffect = false;
            }
        }
    }*/
}
