using HarmonyLib;
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
        }
    }
}
