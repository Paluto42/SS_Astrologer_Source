using AK_DLL;
using Astrologer.Generic;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Astrologer.Laundry
{
    //可以把东西装进这个洗衣机桶洗，洗完对内容物执行效果。一个建筑物可以有多个不同名洗衣机功能，不能有相同功能
    public class TCP_Laundry : CompProperties
    {
        public int interval = 1000;     //完成周期 其他地方都叫interval 不改了，挺好的
        public float effect = 1;        //效果 不一定使用

        //public int requiredLevel = 0;

        public int containerIndex = 1;  //如果洗衣机有多种功能 这里必须指定对应独特id的桶
        public TCP_Laundry()
        {
        }
    }

    public abstract class TC_LaundryBase : ThingComp
    {
        protected TCP_Laundry Prop => props as TCP_Laundry;

        protected int ID => Prop.containerIndex;

        protected TC_ThingContainer linkedContainer = null;

        protected int Interval => Prop.interval;

        protected int tickActived = 0;

        protected float Effect => Prop.effect;

        public bool working = false;    //是否在工作 不工作不tick

        protected virtual string GizmoActiveLabel => "LOF_开关洗衣机";
        protected virtual string GizmoActiveDesc => "LOF_开关洗衣机desc";

        public bool autoHaulManualSwitch = false;   //是否允许小人自动将符合规则的物品丢进桶里。默认是否，也就是必须手动
        //洗衣机的一个功能必须和某个桶绑定
        public TC_ThingContainer LinkedContainer
        {
            get
            {
                if (linkedContainer == null)
                {
                    linkedContainer = parent.GetComp<TC_ThingContainer>();

                    /*TC_ThingContainerManager compManager = parent.GetComp<TC_ThingContainerManager>();

                    if (compManager == null)
                        linkedContainer = parent.GetComp<TC_ThingContainer>();
                    else
                        linkedContainer = compManager.GetContainer(ID);*/

                    if (linkedContainer == null) Log.Error($"[LOF] {parent} 洗衣机没有配套的 {ID}号桶");
                }
                return linkedContainer;
            }
        }

        public virtual bool AnyVacancy
        {
            get
            {
                return LinkedContainer.AnyVacancy;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            Tick(1);
        }
        public override void CompTickLong()
        {
            base.CompTickLong();
            Tick((int)TimeToTick.tickLong);
        }
        public override void CompTickRare()
        {
            base.CompTickRare();
            Tick((int)TimeToTick.tickRare);
        }
        public virtual void Tick(int amt)
        {
            if (!parent.BuildingWorking() || !working) return;
            tickActived += amt;

            if (tickActived >= Interval)
            {
                CompleteWashing();
                tickActived = 0;
            }
        }

        //要写
        public override string CompInspectStringExtra()
        {
            return $"LOF_LaundryWork".Translate(tickActived, Interval);
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            LinkedContainer.EjectAll();
            base.PostDestroy(mode, previousMap);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            //开关洗衣机
            yield return new Command_Action
            {
                defaultDesc = GizmoActiveDesc.Translate(),
                defaultLabel = GizmoActiveLabel.Translate(),
                icon = TypeDef.LaundrySwitch,
                action = delegate ()
                {
                    working = !working;
                }
            };
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref working, "on", false);
            Scribe_Values.Look(ref tickActived, "tick", 0);
        }

        public virtual void CompleteWashing()       //一般应该不改这里
        {
            foreach (Thing i in LinkedContainer.content)
            {
                if (i == null) continue;
                CompleteWashingSingle(i);
            }
            LinkedContainer.EjectAll();
            working = false;
        }

        protected abstract void CompleteWashingSingle(Thing t); //洗完的效果改这里

        public Predicate<Thing> ContentValidator => _ContentValidator;

        protected abstract bool _ContentValidator(Thing t);  //判断某个thing是否是合格的可以放进来洗的
    }
}
