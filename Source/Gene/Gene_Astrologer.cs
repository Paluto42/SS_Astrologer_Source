using AK_DLL;
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

        private int checkInterval = 250; //检测间隔

        private int recoveryInterval = (int)TimeToTick.hour * 1; //失明后的回复周期

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

            List<Hediff> blindness = null;
            if (Utility.CrtTick % checkInterval == 0)
            {
                blindness = pawn.health.hediffSet.hediffs.FindAll(h => h.def == HediffDefOf.Blindness);
                ShouldRecover = blindness.Any();
            }
            if (ShouldRecover) 
            {
                tick++;
                if (tick % recoveryInterval == 0)
                {
                    blindness.ForEach(hediff => hediff.Severity -= 0.1f);
                }
            }
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
