using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    /*public class TCP_MultipleVerb : CompProperties
    {
        public List<VerbProperties> verbPropsList = new();

        public List<string> icons = new();

        public List<string> labels = new();
        public TCP_MultipleVerb()
        {
            compClass = typeof(TC_MultipleVerb);
        }
    }

    public class TC_MultipleVerb : ThingComp 
    {
        private Verb verbInt;

        private List<VerbProperties> availableFireModes = new();

        private CompEquippable compEquippableInt;

        private int currentFireModeIndex = 0;
        public TCP_MultipleVerb Props => (TCP_MultipleVerb)props;

        private CompEquippable EquipmentSource
        {
            get
            {
                if (compEquippableInt != null)
                {
                    return compEquippableInt;
                }
                compEquippableInt = parent.TryGetComp<CompEquippable>();
                if (compEquippableInt == null)
                {
                    Log.ErrorOnce(parent.LabelCap + " has CompSecondaryVerb but no CompEquippable", 50020);
                }
                return compEquippableInt;
            }
        }

        public Pawn CasterPawn => Verb.caster as Pawn;

        private Verb Verb
        {
            get
            {
                verbInt ??= EquipmentSource.PrimaryVerb;
                return verbInt;
            }
        }

        // 这个居然不会自动调用...
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (CasterPawn == null || !CasterPawn.Faction.IsPlayer)
            {
                yield break;
            }

            for (int i = 0; i < availableFireModes.Count; i++)
            {
                string text = Props.icons[i];
                if (string.IsNullOrEmpty(text))
                {
                    text = "UI/Buttons/Reload";
                }
                yield return new Command_Action
                {
                    action = delegate { SwitchToFireMode(i); },
                    defaultLabel = Props.labels[i],
                    icon = ContentFinder<Texture2D>.Get(text, reportFailure: false) 
                };
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            LongEventHandler.ExecuteWhenFinished(InitAvailableFireModes);
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref currentFireModeIndex, "currentFireModeIndex", defaultValue: 0);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                PostVerbDataLoaded();
            }
        }

        private void InitAvailableFireModes()
        {
            availableFireModes.Clear();
            var primaryVerbProps = Verb.verbProps;
            if (primaryVerbProps != null)
            {
                availableFireModes.Add(primaryVerbProps);
            }
            availableFireModes.AddRange(Props.verbPropsList);
            PostVerbDataLoaded();
        }

        private void SwitchToFireMode(int index)
        {
            if (index >= 0 && index < availableFireModes.Count)
            {
                currentFireModeIndex = index;
                EquipmentSource.PrimaryVerb.verbProps = availableFireModes[currentFireModeIndex];
                Log.Message("现在的开火模式为: " + Props.labels[index]);
            }
        }

        private void PostVerbDataLoaded()
        {
            if (currentFireModeIndex >= 0 && currentFireModeIndex < availableFireModes.Count)
            {
                EquipmentSource.PrimaryVerb.verbProps = availableFireModes[currentFireModeIndex];
            }
        }

    }
    */
}
