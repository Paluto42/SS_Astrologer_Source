using AKA_Ability;
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

        public static AbilityDef LOF_VAbility_Astro; //占星师技能容器

        public static AKAbilityDef LOF_Ability_LeechAttack; //吸血攻击

        public static DamageDef LOF_EMP; //改了Fleck的EMP
        public static DamageDef LOF_Cast_ForcePartDestruct; //强制摧毁部件
        public static DamageDef LOF_LeechBomb; //吸血攻击

        public static GeneDef LOF_Gene_Main; //占星师核心基因

        public static HediffDef LOF_Hediff_NebulaRay; //星云射线Buff

        public static JobDef LOF_Job_FillLaundry;
        public static JobDef LOF_Job_InsightInteract; //需要洞察的建筑互动通用这个job

        public static ThingDef LOF_Weapon_AMSR; //ASAT-112
        public static ThingDef LOF_Weapon_Railgun; //ES-117

        public static ThingDef LOF_Thing_Laundy; //环形重塑器
        public static ThingDef LOF_Plant_Starlightgrass; //种地上的 植物 星光草
    }
}
