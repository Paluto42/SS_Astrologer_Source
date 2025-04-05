using AKA_Ability;
using Astrologer.Insight;
using HarmonyLib;
using RimWorld;
using System;
using System.Runtime.CompilerServices;
using Verse;

namespace Astrologer.HarmonyPatches
{
    //招安走的后置Recruit流程，没有OperatorDef只有AKAbility
   /* [HarmonyPatch(typeof(Pawn), "SetFaction", new Type[] { typeof(Faction), typeof(Pawn) })]
    public class Patch_GeneratePostRecruit
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.First)]
        public static void Postfix(Faction newFaction, Pawn recruiter, Pawn __instance)
        {
            if (newFaction != Faction.OfPlayer) return;
            if (__instance == null || __instance.genes.HasAstroGene() == false) return;

            Gene astroGene = __instance.genes.GetAstroGene();
            Ext_OperatorDef operatorDef = astroGene.def.GetModExtension<Ext_OperatorDef>();

            if (operatorDef == null) return;

            Pawn operator_Pawn = __instance;
            VAB_AstroTracker operatorID = Recruit_OperatorID(operator_Pawn);
            //改成招募后才能使用技能容器 
            //我草原来是出生自带一个空文档,导致后面找不到VAB了
            operator_Pawn.TryGetAstroDoc().astroTracker = operatorID;

            Recruit_Ability(operatorID, operatorDef);
        }

        //生成技能容器
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VAB_AstroTracker Recruit_OperatorID(Pawn operator_Pawn)
        {
            AbilityDef operatorID = AstroDefOf.LOF_VAbility_Astro;
            VAB_AstroTracker vAbility = AbilityUtility.MakeAbility(operatorID, operator_Pawn) as VAB_AstroTracker;
            vAbility.AKATracker = new AbilityTracker(operator_Pawn);
            operator_Pawn.abilities.abilities.Add(vAbility);
            return vAbility;
        }

        //添加AKA技能
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Recruit_Ability(VAB_AstroTracker vAbility, Ext_OperatorDef operatorDef)
        {
            if (ModLister.GetActiveModWithIdentifier("ceteam.combatextended") != null) return;
            AbilityTracker tracker = vAbility.AKATracker;
            if (tracker == null)
            {
                Log.Error("Recruit_Ability Failed");
                return;
            }
            if (operatorDef.AKAbilities?.Count > 0)
            {
                operatorDef.AKAbilities.ForEach(def => tracker.AddAbility(def));
            }
        }
    }*/
}
