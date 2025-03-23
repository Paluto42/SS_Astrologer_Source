using RimWorld;
using RimWorld.Planet;
using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Astrologer.Quest
{
    public class Root_GetAdventurerJoin_WalkIn : QuestNode_Root_WandererJoin_WalkIn
    {
        [NoTranslate]
        public SlateRef<string> storeAs;
        public SlateRef<PawnKindDef> mustBeOfKind;

        protected override bool TestRunInt(Slate slate)
        {
            if (slate.TryGet<Pawn>(storeAs.GetValue(slate), out var pawn))
            {
                return true;
            }
            return true;
        }

        public override Pawn GeneratePawn()
        {
            Slate slate = QuestGen.slate;
            PawnKindDef result = mustBeOfKind.GetValue(slate);
            Gender? fixedGender = null;
            if (!slate.TryGet<PawnGenerationRequest>("overridePawnGenParams", out var var))
            {
                var = new PawnGenerationRequest(result, null, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: true, allowDead: false, allowDowned: false, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 20f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: true, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, fixedGender, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Adult, null, null, null, forceRecruitable: true);
            }
            if (Find.Storyteller.difficulty.ChildrenAllowed)
            {
                var.AllowedDevelopmentalStages |= DevelopmentalStage.Child;
            }
            Pawn pawn = PawnGenerator.GeneratePawn(var);
            if (!pawn.IsWorldPawn())
            {
                Find.WorldPawns.PassToWorld(pawn);
            }
            if (storeAs != null)
            {
                QuestGen.slate.Set(storeAs.GetValue(slate), pawn);
            }
            return pawn;
        }
    }
}
