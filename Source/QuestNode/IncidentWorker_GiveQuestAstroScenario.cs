using RimWorld;

namespace Astrologer
{
    public class IncidentWorker_GiveQuestAstroScenario : IncidentWorker_GiveQuest
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            bool result = base.CanFireNowSub(parms);
            if (result && Faction.OfPlayer.def.defName == "")
            {
                return false;
            }
            return result;
        }
    }
}
