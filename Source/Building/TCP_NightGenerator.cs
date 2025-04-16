using RimWorld;
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
    [StaticConstructorOnStartup]
    public class TC_NightGenerator : CompPowerPlant
    {
        private static readonly Vector2 BarSize = new(1.5f, 0.15f);

        private static readonly Material PowerPlantSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f));

        private static readonly Material PowerPlantSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f));
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

        public override void PostExposeData()
        {
            bool powerOnInt = PowerOn;
            Scribe_Values.Look(ref powerOnInt, "powerOn", defaultValue: true);
        }

        public override void PostDraw()
        {
            GenDraw.FillableBarRequest r = default;
            r.center = parent.DrawPos + Vector3.up * 0.1f;
            r.size = BarSize;
            r.fillPercent = PowerOutput / (0f - Props.PowerConsumption);
            r.filledMat = PowerPlantSolarBarFilledMat;
            r.unfilledMat = PowerPlantSolarBarUnfilledMat;
            r.margin = 0.15f;
            Rot4 rotation = parent.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
        }
    }
}
