using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [StaticConstructorOnStartup]
    internal static class Patch_Main
    {
        static Patch_Main()
        {
            Harmony harmony = new("Astrologer.patch");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Type nested = typeof(JobDriver_WatchBuilding).GetNestedType("<>c__DisplayClass2_0", BindingFlags.NonPublic);
            //1.5是<MakeNewToils>b__2
            MethodBase method = nested.GetMethod("<MakeNewToils>b__1", BindingFlags.NonPublic | BindingFlags.Instance);

            harmony.Patch(method, transpiler: new HarmonyMethod(typeof(Patch_JobDriver), nameof(Patch_JobDriver.Transpiler)));
        }
    }
}
