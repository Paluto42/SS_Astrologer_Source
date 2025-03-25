using RimWorld;
using System.Collections.Generic;
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
        public static HairDef LOF_Hair_A;
        public static HairDef LOF_Hair_B;
        public static HairDef LOF_Hair_C;
        public static HairDef LOF_Hair_D;
        public static HairDef LOF_Hair_E;
        public static HairDef LOF_Hair_F;
        public static HairDef LOF_Hair_G;

        public static List<HairDef> AstroHairDefs => new() 
        {
            LOF_Hair_A, LOF_Hair_B, LOF_Hair_C, 
            LOF_Hair_D, LOF_Hair_E, LOF_Hair_F, LOF_Hair_G,
        };

        public static ThingDef LOF_Thing_Laundy;

        public static JobDef LOF_Job_FillLaundry;
        public static JobDef LOF_Job_InsightInteract; //需要洞察的建筑互动通用这个job

        public static GeneDef LOF_Gene_Main;

        public static ThingDef LOF_Plant_Starlightgrass; //种地上的 植物 星光草
    }
}
