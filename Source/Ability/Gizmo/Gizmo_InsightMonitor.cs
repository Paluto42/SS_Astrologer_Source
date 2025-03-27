using Astrologer.Insight;
using UnityEngine;
using Verse;

namespace Astrologer.Ability
{
    public class Gizmo_InsightMonitor : Command
    {
        public const string prefix = "";

        private VAB_AstroTracker compInsights;

        public Gizmo_InsightMonitor(VAB_AstroTracker tracker)
        {
            compInsights = tracker;
        }
        public float InsightInt => compInsights?.insight ?? 0f;
        public float Percent => InsightInt / VAB_AstroTracker.insightCapacity;
        public override float GetWidth(float maxWidth)
        {
            return 120f;
        }

        //记得加翻译字段
        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        {
            Rect rect = new(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
            Rect rect2 = rect.ContractedBy(6f);
            GUI.DrawTexture(rect, BGTex);
            Text.Font = GameFont.Tiny;
            Rect rect3 = rect2.TopHalf();
            Widgets.Label(rect3, prefix + "洞察力".Translate());
            //
            Rect rect4 = rect2.BottomHalf();
            Widgets.FillableBar(rect4, Percent);
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect4, InsightInt + " / " + VAB_AstroTracker.insightCapacity);
            Text.Anchor = TextAnchor.UpperLeft;
            //
            if (Mouse.IsOver(rect) && DoTooltip)
            {
                TipSignal tip = Desc;
                if (disabled && !disabledReason.NullOrEmpty())
                {
                    tip.text += ("\n\n" + "DisabledCommand".Translate() + ": " + disabledReason).Colorize(ColorLibrary.RedReadable);
                }
                tip.text += DescPostfix;
                TooltipHandler.TipRegion(rect, tip);
            }
            return new GizmoResult(GizmoState.Clear);
        }
        public override void ProcessInput(Event ev)
        {
            base.ProcessInput(ev);
        }
        public override void GizmoUpdateOnMouseover()
        {
            base.GizmoUpdateOnMouseover();
        }
    }
}
