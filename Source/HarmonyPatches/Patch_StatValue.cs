using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer.HarmonyPatches
{
    //可切换的StatBase
    [HarmonyPatch(typeof(StatRequest), "StatBases", MethodType.Getter)]
    public class Patch_StatValue
    {
        [HarmonyPostfix]
        public static List<StatModifier> Postfix(List<StatModifier> values, StatRequest __instance)
        {
            if (__instance.Def is not BuildableDef || __instance.Thing is not TransformEquipment eq) return values;

            if (eq.CompFiremode?.Props.overrideStatBase != true) return values;

            VerbProperties verbProps = eq.EquipmentSource?.PrimaryVerb?.verbProps;
            if (verbProps is not null)
            {
                foreach (StatModifier item in values)
                {
                    if (item.stat == StatDefOf.AccuracyTouch) item.value = verbProps.accuracyTouch;
                    if (item.stat == StatDefOf.AccuracyShort) item.value = verbProps.accuracyShort;
                    if (item.stat == StatDefOf.AccuracyMedium) item.value = verbProps.accuracyMedium;
                    if (item.stat == StatDefOf.AccuracyLong) item.value = verbProps.accuracyLong;
                    if (item.stat == StatDefOf.RangedWeapon_Cooldown) item.value = verbProps.defaultCooldownTime;
                }
            }
            return values;
        }
    }
}
