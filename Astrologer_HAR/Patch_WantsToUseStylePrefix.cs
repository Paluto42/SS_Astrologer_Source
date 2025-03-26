using AlienRace;
using Astrologer;
using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace Astrologer_HAR
{
    //傻逼外星人你妈逼的瞎几把Transpile原版基因函数
    /*[HarmonyPatch(typeof(HarmonyPatches), "WantsToUseStylePrefix")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(StyleItemDef), typeof(bool) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref })]
    public class Patch_WantsToUseStylePrefix
    {
        [HarmonyBefore("rimworld.erdelf.alien_race.main")]
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, Pawn pawn, StyleItemDef styleItemDef)
        {
            __result = true;
            if (pawn.genes.HasAstroGene()) 
            {
                return false;
            }
            return true;
        }
    }*/
    [HarmonyPatch(typeof(HarmonyPatches), "WantsToUseStylePostfix")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(StyleItemDef), typeof(bool) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref })]
    public class Patch_WantsToUseStylePostfix
    {
        [HarmonyBefore("rimworld.erdelf.alien_race.main")]
        [HarmonyPrefix]
        public static bool Prefix(Pawn pawn, StyleItemDef styleItemDef)
        {
            if (pawn.genes.HasAstroGene() == false) return true;
            //Log.Message("WantsToUseStylePostfix");//这个方法甚至一次能调用79-120次，sbHAR
            return false;
        }
    }
}
