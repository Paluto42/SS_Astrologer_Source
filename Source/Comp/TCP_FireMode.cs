using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //这个comp是占星师种族专属
    public class TCP_FireMode : CompProperties
    {
        public VerbProperties verbProps = new();

        public string mainIcon = "";

        public string mainWeaponLabel = "";

        public string secondaryIcon = "";

        public string secondaryWeaponLabel = "";
        public TCP_FireMode() 
        {
            compClass = typeof(TC_FireMode);
        }
    }

    public class TC_FireMode : ThingComp 
    {
        private Verb verbInt;

        private CompEquippable compEquippableInt;

        private bool isSecondaryVerbSelected;

        public TCP_FireMode Props => (TCP_FireMode)props;

        public bool IsSecondaryVerbSelected => isSecondaryVerbSelected;

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

        //这个居然不会自动调用...
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (CasterPawn.kindDef.defName == "占星师") 
            {

            }
            if (CasterPawn == null || CasterPawn.Faction.Equals(Faction.OfPlayer))
            {
                string text = (IsSecondaryVerbSelected ? Props.secondaryIcon : Props.mainIcon);
                if (text == "")
                {
                    text = "UI/Buttons/Reload";
                }
                yield return new Command_Action
                {
                    action = SwitchVerb,
                    defaultLabel = (IsSecondaryVerbSelected ? Props.secondaryWeaponLabel : Props.mainWeaponLabel),
                    //defaultDesc = Props.description,
                    icon = ContentFinder<Texture2D>.Get(text, reportFailure: false)
                };
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref isSecondaryVerbSelected, "AS_useSecondaryVerb", defaultValue: false);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                PostAmmoDataLoaded();
            }
        }

        private void SwitchVerb()
        {
            if (!IsSecondaryVerbSelected)
            {
                EquipmentSource.PrimaryVerb.verbProps = Props.verbProps;
                isSecondaryVerbSelected = true;
            }
            else
            {
                EquipmentSource.PrimaryVerb.verbProps = parent.def.Verbs[0];
                isSecondaryVerbSelected = false;
            }
        }

        private void PostAmmoDataLoaded()
        {
            if (isSecondaryVerbSelected)
            {
                EquipmentSource.PrimaryVerb.verbProps = Props.verbProps;
            }
        }

    }
}
