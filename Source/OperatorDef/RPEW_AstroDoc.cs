using AK_DLL;
using Astrologer.Insight;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using AK_TypeDef;

namespace Astrologer
{
    public class RPEW_AstroDoc : RecruitPostEffectWorker_Base
    {
        public RPEW_AstroDoc(OperatorDef def, Pawn operatorPawn) : base(def, operatorPawn)
        {
        }

        public override void RecruitPostEffect()
        {
            VAB_AstroTracker vab_astro = null;
            foreach (RimWorld.Ability vab in operatorPawn.abilities.abilities)
            {
                if (vab is VAB_AstroTracker vab2)
                {
                    vab_astro = vab2;
                }
            }
            operatorPawn.AddDoc<AstroDocument>(new AstroDocument()
            {
                parent = operatorPawn,
                astroTracker = vab_astro,
            });
        }
    }
}
