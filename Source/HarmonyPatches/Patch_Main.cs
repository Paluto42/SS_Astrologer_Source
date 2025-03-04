using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            //harmony.Patch(AccessTools.Property(typeof(Pawn_StoryTracker), "SkinColor").GetGetMethod(), null, new HarmonyMethod(typeof(Patch_SkinColor), "Postfix_get_SkinColor"));
        }
    }
}
