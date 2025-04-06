using AK_DLL;
using AK_TypeDef;
using Astrologer.Insight;
using Verse;

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
