using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer
{
    public class TCP_EMPactGlow : CompProperties
    {
        public TCP_EMPactGlow()
        {
            compClass = typeof(TC_EMPactGlow);
        }
    }

    public class TC_EMPactGlow : ThingComp 
    {
        private Material Material;

        public Material current 
        {
            get 
            {
                if (true)
                {

                }
                return null;
            }
        }
        public override void PostDraw()
        {
            base.PostDraw();
        }
    }
}
