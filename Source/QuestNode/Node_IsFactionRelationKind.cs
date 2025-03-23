using RimWorld;
using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Quest
{
    //判断派系关系
    public class Node_IsFactionRelationKind : QuestNode
    {
        public SlateRef<List<FactionDef>> factionDefs;
        public SlateRef<FactionRelationKind> factionRelationKind;
        public SlateRef<bool> invert = false;

        protected override void RunInt()
        {
        }
        //判断不通过则QuestGen失败
        protected override bool TestRunInt(Slate slate)
        {
            List<FactionDef> value = factionDefs.GetValue(slate);
            foreach (FactionDef item in value)
            {
                Faction faction = Find.FactionManager.FirstFactionOfDef(item);
                if (faction == null)
                {
                    return false;
                }
                if (invert.GetValue(slate))
                {
                    if (faction.RelationKindWith(Faction.OfPlayer) == factionRelationKind.GetValue(slate))
                    {
                        return false;
                    }
                }
                else if (faction.RelationKindWith(Faction.OfPlayer) != factionRelationKind.GetValue(slate))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
