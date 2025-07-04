using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class Bill_ProductionSingle : Bill_Production
    {
        public Bill_ProductionSingle()
        {
        }

        public Bill_ProductionSingle(RecipeDef recipe, Precept_ThingStyle precept = null) : base(recipe, precept)
        {
        }

        protected override void DoConfigInterface(Rect baseRect, Color baseColor)
        {
            Rect rect = new Rect(28f, 32f, 100f, 30f);
            GUI.color = new Color(1f, 1f, 1f, 0.65f);
            Widgets.Label(rect, RepeatInfoText);

            /*GUI.color = baseColor;
            WidgetRow widgetRow = new(baseRect.xMax, baseRect.y + 29f, UIDirection.LeftThenUp);
            if (widgetRow.ButtonText("Details".Translate() + "..."))
            {
                Find.WindowStack.Add(GetBillDialog());
            }

            if (widgetRow.ButtonText(repeatMode.LabelCap.Resolve().PadRight(20)))
            {
                BillRepeatModeUtility.MakeConfigFloatMenu(this);
            }*/

            /*if (widgetRow.ButtonIcon(TexButton.Plus))
            {
                if (repeatMode == BillRepeatModeDefOf.Forever)
                {
                    repeatMode = BillRepeatModeDefOf.RepeatCount;
                    repeatCount = 1;
                }
                else if (repeatMode == BillRepeatModeDefOf.TargetCount)
                {
                    int num = recipe.targetCountAdjustment * GenUI.CurrentAdjustmentMultiplier();
                    targetCount += num;
                    unpauseWhenYouHave += num;
                }
                else if (repeatMode == BillRepeatModeDefOf.RepeatCount)
                {
                    repeatCount += GenUI.CurrentAdjustmentMultiplier();
                }

                SoundDefOf.DragSlider.PlayOneShotOnCamera();
                if (TutorSystem.TutorialMode && repeatMode == BillRepeatModeDefOf.RepeatCount)
                {
                    TutorSystem.Notify_Event(recipe.defName + "-RepeatCountSetTo-" + repeatCount);
                }
            }

            if (widgetRow.ButtonIcon(TexButton.Minus))
            {
                if (repeatMode == BillRepeatModeDefOf.Forever)
                {
                    repeatMode = BillRepeatModeDefOf.RepeatCount;
                    repeatCount = 1;
                }
                else if (repeatMode == BillRepeatModeDefOf.TargetCount)
                {
                    int num2 = recipe.targetCountAdjustment * GenUI.CurrentAdjustmentMultiplier();
                    targetCount = Mathf.Max(0, targetCount - num2);
                    unpauseWhenYouHave = Mathf.Max(0, unpauseWhenYouHave - num2);
                }
                else if (repeatMode == BillRepeatModeDefOf.RepeatCount)
                {
                    repeatCount = Mathf.Max(0, repeatCount - GenUI.CurrentAdjustmentMultiplier());
                }

                SoundDefOf.DragSlider.PlayOneShotOnCamera();
                if (TutorSystem.TutorialMode && repeatMode == BillRepeatModeDefOf.RepeatCount)
                {
                    TutorSystem.Notify_Event(recipe.defName + "-RepeatCountSetTo-" + repeatCount);
                }
            }*/
        }
    }
}
