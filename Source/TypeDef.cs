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
    public static class TypeDef
    {
        public static Texture2D LaundrySwitch => BaseContent.BadTex;
    }

    [DefOf]
    public static class AstroDefOf
    {
        public static ThingDef AS_Laundy;

        public static JobDef AS_Job_FillLaundry;
        public static JobDef AS_Job_InsightInteract; //需要洞察的建筑互动通用这个job

        public static GeneDef Astro_Gene_Main;
    }
}
