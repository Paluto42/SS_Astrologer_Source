using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Plant
{
    //只在夜晚发光的comp
    public class TCP_NightGlower : CompProperties_Glower
    {
        public float skyGlowLowerThan = 0.25f;
        public TCP_NightGlower() 
        {
            compClass = typeof(TC_NightGlower);
        }
    }

    public class TC_NightGlower : CompGlower 
    {
        private TCP_NightGlower Prop => props as TCP_NightGlower;
        float SkyGlowLowerThan => Prop.skyGlowLowerThan;
        protected override bool ShouldBeLitNow 
        {
            get 
            {
                if (!parent.Spawned)
                {
                    return false;
                }
                if (parent?.Map.skyManager.CurSkyGlow > SkyGlowLowerThan)
                {
                    return false;
                }
                if (!FlickUtility.WantsToBeOn(parent))
                {
                    return false;
                }
                if (parent is IThingGlower thingGlower && !thingGlower.ShouldBeLitNow())
                {
                    return false;
                }
                ThingWithComps thingWithComps;
                if ((thingWithComps = parent) != null)
                {
                    foreach (ThingComp allComp in thingWithComps.AllComps)
                    {
                        if (allComp is IThingGlower thingGlower2 && !thingGlower2.ShouldBeLitNow())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public override void CompTickLong()
        {
            base.CompTickLong();
            if (parent != null)
            {
                UpdateLit(parent?.Map);
            }
        }
    }
}
