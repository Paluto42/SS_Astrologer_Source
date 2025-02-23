using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.HarmonyPatches
{
    [StaticConstructorOnStartup]
    internal class Patch_Main
    {
        public Patch_Main() 
        {
            Harmony harmony = new("Astrologer.patch");
            harmony.PatchAll();
        }
    }
}
