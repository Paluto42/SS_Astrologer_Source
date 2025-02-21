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
    //只在夜晚发电的电机
    public class TCP_NightGenerator : CompProperties_Power
    {
        //public float nightPowerRatio = 0;

        public float skyGlowLowerThan = 0.2f; //天空光亮低于这么多就视为夜晚
        public TCP_NightGenerator()
        {
            compClass = typeof(TC_NightGenerator);
        }
    }
    public class TC_NightGenerator : CompPowerPlantSolar
    {
        private TCP_NightGenerator Prop => props as TCP_NightGenerator;

        //private float NightPower => Prop.nightPowerRatio * Prop.PowerConsumption;

        float SkyGlowLowerThan => Prop.skyGlowLowerThan;
        protected override float DesiredPowerOutput
        {
            get
            {
                if (parent.Map == null || parent.Map.skyManager.CurSkyGlow > SkyGlowLowerThan) return 0;
                return Mathf.Lerp(0f, 0f - Props.PowerConsumption, 1f - parent.Map.skyManager.CurSkyGlow) * RoofedPowerOutputFactor;
            }
        }

        private float RoofedPowerOutputFactor
        {
            get
            {
                int num = 0;
                int num2 = 0;
                foreach (IntVec3 item in parent.OccupiedRect())
                {
                    num++;
                    if (parent.Map.roofGrid.Roofed(item))
                    {
                        num2++;
                    }
                }
                return (float)(num - num2) / (float)num;
            }
        }

        public override void PostDraw()
        {
            base.PostDraw();
        }
    }
}
