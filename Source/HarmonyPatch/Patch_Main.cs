using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    [StaticConstructorOnStartup]
    internal class Patch_Main
    {
        public Patch_Main() 
        {
            Harmony harmony = new Harmony("Astrologer.patch");
            harmony.PatchAll();
        }
    }
}
