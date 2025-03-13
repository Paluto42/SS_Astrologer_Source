using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class AS_ModSettings : ModSettings
    {
    }

    public class AS_Mod : Mod
    {
        public AS_Mod(ModContentPack content) : base(content)
        {
        }
        public override string SettingsCategory()
        {
            return "Astrologer";
        }
    }
}
