using HarmonyLib;
using RimWorld;

namespace Astrologer.HarmonyPatches
{
    [HarmonyPatch(typeof(BillStack), "AddBill")]
    public class Patch_BillStack
    {
        [HarmonyPostfix]
        public static void Postfix(BillStack __instance) 
        {
            if (__instance.billGiver is Building_CraftingTable table) 
            {
                table.Notify_BillAdded();
            }
        }
    }
}
