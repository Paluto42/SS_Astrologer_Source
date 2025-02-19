using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Astrologer.Generic
{
    //通用的，可以存储一组物品的comp
    [StaticConstructorOnStartup]
    public class TCP_ThingContainer : CompProperties
    {
        public int capacity = 1;

        public bool showEjectGizmo = true;

        public int uniqueID = 1;                                //有多个桶时 检索的独特id 就一个桶就随便

        public string gizmoEjectLabel = "SSR_Eject";            //有多个桶的话gizmo label不能重复
        public string gizmoEjectDesc = "SSR_EjectDesc";
        public string gizmoEjectUIIconPath = "UI/TempIcon";

        public string btnITabLabel = "SSR_Bucket";              //左下角itab按钮的名字
        public string btnITabItemKey = "ContainedItems";        //itab点开最上面显示那个，默认是"内容物"

        public TCP_ThingContainer()
        {
            compClass = typeof(TC_ThingContainer);
        }
    }

    public class TC_ThingContainer : ThingComp, IThingHolder
    {
        #region 基建
        public TCP_ThingContainer Prop => props as TCP_ThingContainer;

        public int ID => Prop.uniqueID;

        public int ContentsCount => content.Count;
        public int Capacity => Prop.capacity;
        protected bool ShowGizemo => Prop.showEjectGizmo;
        public bool AnyVacancy => Capacity > ContentsCount;

        protected Command_Action cachedGizmo = null;

        public ThingOwner<Thing> content;

        public TC_ThingContainer()
        {
            this.content = new ThingOwner<Thing>(this);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (!ShowGizemo || parent == null || parent.Map == null) yield break;
            //fixme
            if (cachedGizmo == null)
            {
                cachedGizmo = new Command_Action
                {
                    defaultLabel = Prop.gizmoEjectLabel.Translate(),
                    defaultDesc = Prop.gizmoEjectDesc.Translate(),
                    icon = ContentFinder<Texture2D>.Get(Prop.gizmoEjectUIIconPath),
                    action = EjectAll
                };
            }
            yield return cachedGizmo;
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return content;
        }
        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, GetDirectlyHeldThings());
        }
        #endregion

        public void EjectAll()
        {
            Map map = parent.Map;
            if (map == null) return;
            content.TryDropAll(parent.InteractionCell, map, ThingPlaceMode.Near);
        }

        public bool TryAccept(Thing t)
        {
            if (!content.CanAcceptAnyOf(t)) return false;
            if (!AnyVacancy) return false;

            if (t.holdingOwner != null)
            {
                t.holdingOwner.TryTransferToContainer(t, content, t.stackCount);
            }
            else
            {
                content.TryAdd(t);
            }
            return true;
        }

        public override void PostExposeData()
        {
            Scribe_Deep.Look(ref content, "content", this);
        }
    }
}