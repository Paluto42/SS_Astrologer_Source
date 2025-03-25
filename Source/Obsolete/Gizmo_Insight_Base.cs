using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //可以当作其他洞察力技能的Base
    //技能换akability
    /*public abstract class Gizmo_Insight_Base : Command
    {
        public TC_Insights compInsights;

        public string prefix = "";
        public override float GetWidth(float maxWidth)
        {
            return 120f;
        }
        public float Percent 
        {
            get 
            { 
                return compInsights.CurInsights / (float)compInsights.MaxInsights; 
            }
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
            Widgets.Label(rect4, compInsights.CurInsights + " / " + compInsights.MaxInsights);
            Text.Anchor = TextAnchor.UpperLeft;
            return new GizmoResult(GizmoState.Clear);
        }

    }*/
}
