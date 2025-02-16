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
    //和TC_Insight联动的Comp
    public class TCP_FireMode : CompProperties
    {
        public VerbProperties verbProps = new();

        public int consumeInsight = 1;

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
        private TC_Insights CompInsight => CasterPawn?.GetComp<TC_Insights>();

        private Verb verbInt;

        private CompEquippable compEquippableInt;

        private bool isSecondaryVerbSelected = false;

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
            if (CasterPawn == null || CasterPawn.Faction.Equals(Faction.OfPlayer))
            {
                string text = (IsSecondaryVerbSelected ? Props.secondaryIcon : Props.mainIcon);
                if (text == "")
                {
                    text = "UI/Buttons/Reload";
                }
                if (CompInsight == null) yield break;
                //洞察力不足强制切回主Verb显示
                if (CompInsight.CurInsights < Props.consumeInsight) 
                {
                    if (isSecondaryVerbSelected) 
                    {
                        SwitchVerb();
                        string translatedMessage = "洞察力不足了!".Translate();
                        MoteMaker.ThrowText(CasterPawn.PositionHeld.ToVector3(), CasterPawn.MapHeld, translatedMessage, 3f);
                    }
                    yield return new Command_Action
                    {
                        action = delegate { },
                        defaultLabel = Props.mainWeaponLabel,
                        icon = ContentFinder<Texture2D>.Get(Props.mainIcon, reportFailure: false)
                    };
                    yield break;
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
        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
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
                Log.Message("现在的verb是 SecondaryVerb: " + EquipmentSource.PrimaryVerb);
            }
            else
            {
                EquipmentSource.PrimaryVerb.verbProps = parent.def.Verbs[0];
                isSecondaryVerbSelected = false;
                Log.Message("现在的verb是 PrimaryVerb: " + EquipmentSource.PrimaryVerb);
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
