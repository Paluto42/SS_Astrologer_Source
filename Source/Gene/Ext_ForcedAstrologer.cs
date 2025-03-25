using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class Ext_ForcedAstrologer : DefModExtension
    {
        public BodyTypeDef forcedBodyType;
        public AbilityDef astroAbility;  //舟同款，电子身份证
        public Color? skinColorOverride;
    }
}
