using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Astrologer.HarmonyPatches
{
    //孩子们我是闭包委托,一句话封装一个类
    public static class Patch_JobDriver
    {
        // <>c__DisplayClass2_0
        public static readonly Type nestedAction = typeof(JobDriver_WatchBuilding).GetNestedType("<>c__DisplayClass2_0", BindingFlags.NonPublic);

        public static void TryGainMemory(Pawn pawn, Job job)
        {
            if (job.def.defName != "UseTelescope" || pawn.TryGetAstroDoc() == null) return;
            pawn.needs.mood.thoughts.memories.TryGainMemory(AstroDefOf.LOF_Thought_UseTelescopeMood);
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();
            //IL_000b
            int index = codes.FindIndex(code => code.opcode == OpCodes.Call);
            if (index != -1)
            {
                MethodInfo delegateMethod = typeof(Patch_JobDriver).GetMethod(nameof(TryGainMemory));
                codes.InsertRange(index, new CodeInstruction[]
                {
                    new (OpCodes.Ldarg, 0),
                    new (OpCodes.Ldfld, nestedAction.GetField("<>4__this", BindingFlags.Instance | BindingFlags.Public)),
                    new (OpCodes.Ldfld, typeof(JobDriver).GetField("pawn", BindingFlags.Instance | BindingFlags.Public)),
                    new (OpCodes.Ldarg, 0),
                    new (OpCodes.Ldfld, nestedAction.GetField("<>4__this", BindingFlags.Instance | BindingFlags.Public)),
                    new (OpCodes.Ldfld,  typeof(JobDriver).GetField("job", BindingFlags.Instance | BindingFlags.Public)),
                    new (OpCodes.Call, delegateMethod)
                });
            }
            return codes;
        }
    }
}
