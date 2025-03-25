using Astrologer.Insight;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer
{
    //用来生成一个定时产生EMP脉冲爆炸的临时Post处理物
    public class VP_EMPpop : VerbProperties 
    {
        public ThingDef postSpawnThing = null;
        public VP_EMPpop()
        {
            verbClass = typeof(Verb_EMPpop);
        }
    }
    public class Verb_EMPpop : Verb_PreApplyShoot
    {
        VP_EMPpop Props => (VP_EMPpop)verbProps;
        protected override bool TryCastShot()
        {
            base.TryCastShot();
            Pop();
            if (burstShotsLeft <= 1)
            {
                SelfConsume();
            }
            return true;
        }
        public void Pop()
        {
            float radius = Caster.GetStatValue(StatDefOf.PackRadius);
            GenExplosion.DoExplosion(currentTarget.Cell, base.CasterPawn.Map, radius, DamageDefOf.EMP, null, -1, -1f, null, null, null, null, Props.postSpawnThing, 1f, 1, GasType.BlindSmoke);
        }
        public override void Notify_EquipmentLost()
        {
            base.Notify_EquipmentLost();
            if (state == VerbState.Bursting && burstShotsLeft < verbProps.burstShotCount)
            {
                SelfConsume();
            }
        }

        private void SelfConsume()
        {
            if (base.EquipmentSource != null && !base.EquipmentSource.Destroyed)
            {
                base.EquipmentSource.Destroy();
            }
        }
    }
}
