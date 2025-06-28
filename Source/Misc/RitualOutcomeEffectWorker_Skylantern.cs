namespace Astrologer
{
    /*public class RitualEffectWorker_Skylantern : RitualOutcomeEffectWorker_Skylantern
    {
        public RitualEffectWorker_Skylantern()
        {
        }

        public RitualEffectWorker_Skylantern(RitualOutcomeEffectDef def)
            : base(def)
        {
        }

        public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
        {
            base.Apply(progress, totalPresence, jobRitual);
            MC_Astrologer mapComponent = (MC_Astrologer)jobRitual.Map.components.FirstOrDefault(mc => mc is MC_Astrologer);
            if (mapComponent == null) return;
            mapComponent.skyLanternRitualEffect = true;
        }
    }*/
}
