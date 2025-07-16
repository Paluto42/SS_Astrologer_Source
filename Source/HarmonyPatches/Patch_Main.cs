using HarmonyLib;
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

            Type nested = Patch_JobDriver.nestedAction;
#if ver16
            MethodBase method = nested.GetMethod("<MakeNewToils>b__1", BindingFlags.NonPublic | BindingFlags.Instance);
#endif
#if ver15
            MethodBase method = nested.GetMethod("<MakeNewToils>b__2", BindingFlags.NonPublic | BindingFlags.Instance);
#endif
            harmony.Patch(method, transpiler: new HarmonyMethod(typeof(Patch_JobDriver), nameof(Patch_JobDriver.Transpiler)));
            Log.Message("Astrologer Welcum");
        }
    }
}
