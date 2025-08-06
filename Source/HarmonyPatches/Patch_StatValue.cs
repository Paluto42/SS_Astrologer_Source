using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Astrologer.HarmonyPatches
{
    //通过Verb切换StatBaseValue
    [HarmonyPatch(typeof(StatRequest), "StatBases", MethodType.Getter)]
    public class Patch_StatValue
    {
        [HarmonyPostfix]
        public static List<StatModifier> Postfix(List<StatModifier> values, StatRequest __instance)
        {
            if (__instance.Def is not BuildableDef || __instance.Thing is not TransformEquipment eq) return values;

            if (eq.CompFiremode?.Props.overrideStatBase != true) return values;

            VP_StatValue verbProps = eq.EquipmentSource?.PrimaryVerb?.verbProps as VP_StatValue;
            if (verbProps is null) return values;

            return verbProps.statBases;
        }
    }

    /*[HarmonyPatch(typeof(StatWorker), "GetBaseValueFor")]
    public class Patch_StatValue
    {
        public static void GetStatBase(ref StatRequest request, StatDef stat, ref float value) 
        {
            if (request.Def is not BuildableDef || request.Thing is not TransformEquipment eq) return;
            if (eq.CompFiremode?.Props.overrideStatBase != true) return;
            VerbProperties verbProps = eq.EquipmentSource?.PrimaryVerb?.verbProps;
            if (verbProps is not null)
            {
                if (stat == StatDefOf.AccuracyTouch) 
                    value = verbProps.accuracyTouch;
                if (stat == StatDefOf.AccuracyShort) 
                    value = verbProps.accuracyShort;
                if (stat == StatDefOf.AccuracyMedium) 
                    value = verbProps.accuracyMedium;
                if (stat == StatDefOf.AccuracyLong) 
                    value = verbProps.accuracyLong;
                if (stat == StatDefOf.RangedWeapon_Cooldown) 
                    value = verbProps.defaultCooldownTime;
            }
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase original)
        {
            CodeMatcher codeMatcher = new(instructions);
            codeMatcher.Start();
            codeMatcher.MatchStartForward(new CodeMatch(OpCodes.Blt_S))
                .InsertAndAdvance(
                new(OpCodes.Ldarga_S, 1),
                new(OpCodes.Ldarg, 0),
                new(OpCodes.Ldfld, typeof(StatWorker).GetField("stat", BindingFlags.Instance | BindingFlags.NonPublic)),
                new(OpCodes.Ldloca, 0),
                new(OpCodes.Call, AccessTools.Method(typeof(Patch_StatValue), nameof(GetStatBase))));

            return codeMatcher.Instructions();
        }
    }*/
}
