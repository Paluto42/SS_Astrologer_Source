using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [StaticConstructorOnStartup]
    public static class Patch_Main
    {
        static Patch_Main()
        {
            Harmony harmony = new("Astrologer.patch");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            Type nested = typeof(JobDriver_WatchBuilding).GetNestedType("<>c__DisplayClass2_0", BindingFlags.NonPublic);
            MethodBase method = nested.GetMethod("<MakeNewToils>b__2", BindingFlags.NonPublic | BindingFlags.Instance);

            harmony.Patch(method, transpiler: new HarmonyMethod(typeof(Patch_JobDriver), nameof(Patch_JobDriver.Transpiler)));
        }
    }
}
