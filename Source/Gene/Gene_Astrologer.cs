using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Astrologer
{
    //准备删了
    public class Gene_Astrologer : Gene
    {
        private int tick = 0;
        private bool ShouldRecover = false;
        private const int checkInterval = 250; //检测间隔
        private const float recoveryAmount = 0.1f; //每次回复量
        private const int recoveryInterval = 2500; //失明后的回复周期

        private List<Hediff> BlindnessTmp => pawn.health.hediffSet.hediffs.FindAll(h => h.def == HediffDefOf.Blindness);

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref tick, "recoveryTick");
        }

        //眼睛会独立恢复
        public override void Tick()
        {
            base.Tick();
            if (pawn.Dead) return;

            if (Utility.CrtTick % checkInterval == 0) this.ShouldRecover = BlindnessTmp.Any();
            if (this.ShouldRecover)
            {
                tick++;
                if (tick % recoveryInterval == 0)
                {
                    BlindnessTmp.First().Severity -= recoveryAmount;
                    tick = 0;
                }
            }
            else tick = 0;
        }

        public override void PostAdd()
        {
            /*Ext_ForcedAstrologer ext = def.GetModExtension<Ext_ForcedAstrologer>();
            if (ext != null && ext.forcedBodyType != null)
            {
                pawn.story.bodyType = ext.forcedBodyType;
            }
            //文档给的是个空VAB一定要替换掉!!!
            pawn.AddDoc<AstroDocument>(new AstroDocument()
            {
                parent = pawn,
                astroTracker = new VAB_AstroTracker(pawn, ext.astroAbility),
            });*/
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }
    }
}
