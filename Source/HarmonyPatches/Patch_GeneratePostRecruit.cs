using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using AK_DLL;
using AK_TypeDef;
using AKA_Ability;

namespace Astrologer.HarmonyPatches
{
    //招安走的后置Recruit流程，没有OperatorDef只有AKAbility
    [HarmonyPatch(typeof(Pawn), "SetFaction", new Type[] { typeof(Faction), typeof(Pawn) } )]
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
            VAbility_AKATrackerContainer operatorID = Recruit_OperatorID(operator_Pawn);
            //operator_Pawn.AddDoc(new OpDocContainer(operator_Pawn) { va = operatorID });

            Recruit_Ability(operatorID, operatorDef);
        }
        private static VAbility_AKATrackerContainer Recruit_OperatorID(Pawn operator_Pawn)
        {
            //舟档案系统 占星师不需要
            /*string OperatorID = "LOF" + "_" + operator_Pawn.GetHashCode().ToString();
            GC_OperatorDocumentation.AddPawn(OperatorID, null, operator_Pawn, weapon: null, null);
            OperatorDocument document = GC_OperatorDocumentation.opDocArchive[OperatorID];
            document.voicePack = null;
            AbilityDef operatorID = AKDefOf.AK_VAbility_Operator;*/

            AbilityDef operatorID = AstroDefOf.LOF_VAbility_Astro;
            VAbility_AKATrackerContainer vAbility = AbilityUtility.MakeAbility(operatorID, operator_Pawn) as VAbility_AKATrackerContainer;
            vAbility.AKATracker = new AbilityTracker(operator_Pawn);//使用AK_AbilityTracker会报错
            operator_Pawn.abilities.abilities.Add(vAbility);
            return vAbility;
        }

        private static void Recruit_Ability(VAbility_AKATrackerContainer vanillaAbility, Ext_OperatorDef operatorDef)
        {
            if (ModLister.GetActiveModWithIdentifier("ceteam.combatextended") != null) return;
            AbilityTracker tracker = vanillaAbility.AKATracker;
            if (operatorDef.AKAbilities?.Count > 0)
            {
                operatorDef.AKAbilities.ForEach(def => tracker.AddAbility(def));
            }
        }
    }
}
