using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Astrologer.Insight
{
    public enum BurstFireStatus
    {
        None = 0,
        Started = 1,
        Completed = 2
    }

    //和TC_Insight联动的Comp
    public class TCP_FireMode : CompProperties
    {
        public VerbProperties verbProps;
        public GraphicData graphicData; //变形贴图

        public bool overrideStatBase = false;
        public int consumeAmount = 1;
        public int consumeDuration = 0;
        public string mainIcon;
        public string mainWeaponLabel;
        public string mainDescription;
        public string secondaryIcon;
        public string secondaryWeaponLabel;
        public string secondaryDescription;
        public TCP_FireMode()
        {
            compClass = typeof(TC_FireMode);
        }
    }

    //每发子弹都消耗洞察力
    public class TC_FireMode : ThingComp
    {
        public TCP_FireMode Props => (TCP_FireMode)props;
        public GraphicData GraphicData => Props.graphicData;
        public TransformEquipment Equipment => parent as TransformEquipment;
        private CompEquippable EquipmentSource => Equipment.EquipmentSource;
        public Pawn CasterPawn => (EquipmentSource?.ParentHolder as Pawn_EquipmentTracker)?.pawn;
        private VAB_AstroTracker CompInsight => CasterPawn?.TryGetAstroTracker();//洞察力comp

        private bool isSecondaryVerbSelected = false;
        public bool IsSecondaryVerbSelected => isSecondaryVerbSelected;
        public bool ShouldCalulateBursts => Props.consumeDuration > 0;

        public BurstFireStatus burstStatus = BurstFireStatus.None;

        private int burstTick = 0;

        //打完这一梭子以后过一段时间才会扣洞察力
        public override void CompTick()
        {
            if (!isSecondaryVerbSelected || burstStatus != BurstFireStatus.Started) return;
            burstTick++;
            if (burstTick >= Props.consumeDuration)
            {
                burstTick = 0;
                burstStatus = BurstFireStatus.Completed;
                CompInsight.ConsumeInsight(Props.consumeAmount);
            }
        }

        public void Notify_Launched()
        {
            if (!isSecondaryVerbSelected) return;
            if (!ShouldCalulateBursts) CompInsight.ConsumeInsight(Props.consumeAmount);
            //if (burstStatus != BurstFireStatus.Started) return;
            /*if (burstCount == 0) CompInsight.ConsumeInsight(Props.consumeAmount);
            burstCount++;
            if (burstCount >= Verb.verbProps.burstShotCount)
            {
                burstCount = 0;
                burstStatus = BurstFireStatus.Completed;
            }*/
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
                    string message = "LOF_NeedInsight!".Translate();
                    MoteMaker.ThrowText(CasterPawn.PositionHeld.ToVector3(), CasterPawn.MapHeld, message, 3f);
                }
                yield return new Command_Action//只能显示主Verb
                {
                    action = delegate
                    {
                        Messages.Message("LOF_NeedInsight".Translate(), MessageTypeDefOf.RejectInput);
                    },
                    defaultLabel = Props.mainWeaponLabel,
                    defaultDesc = Props.mainDescription + "\n" + "LOF_InsufficientInsight".Translate(Props.consumeAmount).Colorize(ColorLibrary.Yellow),
                    icon = ContentFinder<Texture2D>.Get(Props.mainIcon, reportFailure: false)
                };
                yield break;
            }
            yield return new Command_Action//主副Verb同时切换显示
            {
                action = SwitchVerb,
                defaultLabel = IsSecondaryVerbSelected ? Props.secondaryWeaponLabel : Props.mainWeaponLabel,
                defaultDesc = IsSecondaryVerbSelected ? Props.secondaryDescription : Props.mainDescription + "\n" + "LOF_InsufficientInsight".Translate(Props.consumeAmount).Colorize(ColorLibrary.Yellow),
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
            Scribe_Values.Look(ref isSecondaryVerbSelected, "useSecondaryVerb", defaultValue: false);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (isSecondaryVerbSelected) EquipmentSource.PrimaryVerb.verbProps = Props.verbProps;
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

    }
}
