using AK_DLL;
using AK_TypeDef;
using AKA_Ability;
using Astrologer.Insight;
using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace Astrologer.HarmonyPatches
{
    //招安走的后置Recruit流程，没有OperatorDef只有AKAbility
    [HarmonyPatch(typeof(Pawn), "SetFaction", new Type[] { typeof(Faction), typeof(Pawn) })]
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
            //改成招募后才能使用技能,而不是出生自带
            /*Ext_ForcedAstrologer ext = astroGene.def.GetModExtension<Ext_ForcedAstrologer>();
            VAB_AstroTracker astroTracker = new(operator_Pawn, ext.astroAbility);
            operator_Pawn.AddDoc(new AstroDocument()
            {
                parent = operator_Pawn,
                astroTracker = astroTracker
            });*/
            Recruit_Ability(operatorID, operatorDef);
        }
        private static VAbility_AKATrackerContainer Recruit_OperatorID(Pawn operator_Pawn)
        {
            //舟档案系统 占星师不需要
            /*string OperatorID = "LOF" + "_" + operator_Pawn.GetHashCode().ToString();
            GC_OperatorDocumentation.AddPawn(OperatorID, null, operator_Pawn, weapon: null, null);
            OperatorDocument document = GC_OperatorDocumentation.opDocArchive[OperatorID];
            document.voicePack = null;;*/

            //AbilityDef operatorID = AKDefOf.AK_VAbility_Operator;
            AbilityDef operatorID = AstroDefOf.LOF_VAbility_Astro;
            VAbility_AKATrackerContainer vAbility = AbilityUtility.MakeAbility(operatorID, operator_Pawn) as VAbility_AKATrackerContainer;
            vAbility.AKATracker = new AbilityTracker(operator_Pawn);
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
