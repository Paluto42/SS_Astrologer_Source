using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Astrologer.Insight
{
    public enum FireTickStatus
    {
        None = 0,
        Started = 1,
        Completed = 2
    }
    //和TC_Insight联动的Comp
    public class TCP_newFireMode : CompProperties
    {
        public VerbProperties verbProps = new();

        public int consumeAmount = 1;
        public int consumeDuration = 0;

        public string mainIcon;
        public string mainWeaponLabel;
        public string mainDescription;
        public string secondaryIcon;
        public string secondaryWeaponLabel;
        public string secondaryDescription;
        public TCP_newFireMode()
        {
            compClass = typeof(TC_newFireMode);
        }
    }
    //每发子弹都消耗洞察力
    public class TC_newFireMode : ThingComp
    {
        public FireTickStatus tickStatus = FireTickStatus.None;
        private VAB_AstroTracker CompInsight => CasterPawn?.TryGetAstroTracker();
        //private TC_Insights CompInsight => CasterPawn?.GetComp<TC_Insights>();//洞察力comp

        private Verb verbInt;

        private CompEquippable compEquippableInt;

        private bool isSecondaryVerbSelected = false;
        public TCP_newFireMode Props => (TCP_newFireMode)props;
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

        public Pawn CasterPawn => Verb.CasterPawn;

        private Verb Verb
        {
            get
            {
                verbInt ??= EquipmentSource.PrimaryVerb;
                return verbInt;
            }
        }
        public override void CompTick()
        {
            if (tickStatus != FireTickStatus.Started) return;
            if (isSecondaryVerbSelected && Utility.IsTickInterval(Props.consumeDuration))
            {
                tickStatus = FireTickStatus.Completed;
            }
        }
        //这个居然不会自动调用...
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (CasterPawn == null || CasterPawn?.Faction.Equals(Faction.OfPlayer) == false)
            {
                yield break;
            }
            string text = IsSecondaryVerbSelected ? Props.secondaryIcon : Props.mainIcon;
            if (string.IsNullOrEmpty(text))
            {
                text = "UI/Buttons/Reload";
            }
            //洞察力不足或缺失comp强制切回主Verb显示
            if (CompInsight == null || CompInsight.insight < Props.consumeAmount)
            {
                if (isSecondaryVerbSelected)
                {
                    SwitchVerb();
                    string message = "洞察力不足了!".Translate();
                    MoteMaker.ThrowText(CasterPawn.PositionHeld.ToVector3(), CasterPawn.MapHeld, message, 3f);
                }
                yield return new Command_Action//只能显示主Verb
                {
                    action = delegate { },
                    defaultLabel = Props.mainWeaponLabel,
                    defaultDesc = Props.mainDescription,
                    icon = ContentFinder<Texture2D>.Get(Props.mainIcon, reportFailure: false)
                };
                yield break;
            }
            yield return new Command_Action//主副Verb同时切换显示
            {
                action = SwitchVerb,
                defaultLabel = IsSecondaryVerbSelected ? Props.secondaryWeaponLabel : Props.mainWeaponLabel,
                defaultDesc = IsSecondaryVerbSelected ? Props.secondaryDescription : Props.mainDescription,
                icon = ContentFinder<Texture2D>.Get(text, reportFailure: false)
            };

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
