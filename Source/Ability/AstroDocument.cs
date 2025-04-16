using AK_DLL.Document;
using Astrologer.Insight;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //装备或者技能的配套id
    public static class EffectIDs
    {
        public static string halfIgnoreDmg = "halfIgnoreDmg"; //朦胧暗月
        public static string ignoreStagger = "ignoreStagger"; //TLK-11 "凤凰座' 远行装甲 被击中不会影响移动
        public static string incomingDmgFactor = "incomingDmgFactor"; //承伤系数变低
    }
    public class AstroDocument : DocumentBase
    {
        public VAB_AstroTracker astroTracker;

        //一些技能和装备的效果以string注册在这里，int是持续到多少tick
        //总量少 没做回收
        public Dictionary<string, int> effects = new();

        public AstroDocument() : base() { }
        public AstroDocument(Thing parent) : base(parent)
        {
        }

        public bool EffectValid(string effectID)
        {
            effects.TryGetValue(effectID, out var effectiveUntilTick);
            if (effectiveUntilTick < Utility.CrtTick) return false; //default int是0
            return true;
        }

        public void EffectRefresh(string effectID, int duration)
        {
            if (!effects.ContainsKey(effectID)) effects.Add(effectID, duration);
            effects[effectID] = Mathf.Max(effects[effectID], Utility.CrtTick + duration);
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref astroTracker, "tracker");
        }
    }
}
