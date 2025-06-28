using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer
{
    public class ScenPart_GameStartLetter : ScenPart
    {
        public string label = "";
        public string description = "";

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }
            if (label == null)
            {
                yield return GetType()?.ToString() + " has null lableKey.";
            }
            if (description == null)
            {
                yield return GetType()?.ToString() + " has null lableKey.";
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref label, "label");
            Scribe_Values.Look(ref description, "description");
        }

        public override void PostGameStart()
        {
            Find.LetterStack.ReceiveLetter(label, description, LetterDefOf.NeutralEvent, debugInfo: null);
        }
    }
}
