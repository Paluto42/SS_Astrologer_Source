using RimWorld;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public static class TypeDef
    {
        public static Texture2D LaundrySwitch => BaseContent.BadTex;
    }

    [DefOf]
    public static class AstroDefOf
    {
        public static ThingDef LOF_Thing_Laundy;

        public static JobDef LOF_Job_FillLaundry;
        public static JobDef LOF_Job_InsightInteract; //需要洞察的建筑互动通用这个job

        public static GeneDef LOF_Gene_Main;
    }
}
