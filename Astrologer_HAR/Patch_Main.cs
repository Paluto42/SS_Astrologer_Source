using HarmonyLib;
using System.Reflection;
using Verse;

namespace Astrologer_HAR
{
    [StaticConstructorOnStartup]
    internal class Patch_Main
    {
        static Patch_Main()
        {
            Harmony harmony = new Harmony("Astrologer.har.patch");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("[Astrologer] qnmd HAR");
        }
    }
}
