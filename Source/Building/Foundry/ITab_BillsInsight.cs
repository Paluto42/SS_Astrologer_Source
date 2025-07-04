using LudeonTK;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Astrologer
{
    //需要洞察力,只能添加单个单次配方的清单面板
    public class ITab_BillsInsight : ITab
    {
        protected float viewHeight = 1000f;

        protected Vector2 scrollPosition;

        protected Bill mouseoverBill;

        protected static readonly Vector2 WinSize = new(420f, 480f);

        [TweakValue("Interface", 0f, 128f)]
        protected static float PasteX = 48f;

        [TweakValue("Interface", 0f, 128f)]
        protected static float PasteY = 3f;

        [TweakValue("Interface", 0f, 32f)]
        protected static float PasteSize = 24f;
        protected Building_CraftingTable SelTable => (Building_CraftingTable)base.SelThing;

        public ITab_BillsInsight()
        {
            size = WinSize;
            labelKey = "TabBills";
            tutorTag = "Bills";
        }

        protected override void FillTab()
        {
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.BillsTab, KnowledgeAmount.FrameDisplayed);
            Rect rect2 = new(WinSize.x - PasteX, PasteY, PasteSize, PasteSize);
            // 复制清单
            /*if (BillUtility.Clipboard != null)
            {
                if (!SelTable.def.AllRecipes.Contains(BillUtility.Clipboard.recipe) || !BillUtility.Clipboard.recipe.AvailableNow || !BillUtility.Clipboard.recipe.AvailableOnNow(SelTable))
                {
                    GUI.color = Color.gray;
                    Widgets.DrawTextureFitted(rect2, TexButton.Paste, 1f);
                    GUI.color = Color.white;
                    if (Mouse.IsOver(rect2))
                    {
                        TooltipHandler.TipRegion(rect2, "ClipboardBillNotAvailableHere".Translate() + ": " + BillUtility.Clipboard.LabelCap);
                    }
                }
                else if (SelTable.billStack.Count >= 15)
                {
                    GUI.color = Color.gray;
                    Widgets.DrawTextureFitted(rect2, TexButton.Paste, 1f);
                    GUI.color = Color.white;
                    if (Mouse.IsOver(rect2))
                    {
                        TooltipHandler.TipRegion(rect2, "PasteBillTip".Translate() + " (" + "PasteBillTip_LimitReached".Translate() + "): " + BillUtility.Clipboard.LabelCap);
                    }
                }
                else
                {
                    if (Widgets.ButtonImageFitted(rect2, TexButton.Paste, Color.white))
                    {
                        Bill bill = BillUtility.Clipboard.Clone();
                        bill.InitializeAfterClone();
                        SelTable.billStack.AddBill(bill);
                        SoundDefOf.Tick_Low.PlayOneShotOnCamera();
                    }
                    if (Mouse.IsOver(rect2))
                    {
                        TooltipHandler.TipRegion(rect2, "PasteBillTip".Translate() + ": " + BillUtility.Clipboard.LabelCap);
                    }
                }
            }*/
            Rect rect3 = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
            mouseoverBill = SelTable.billStack.DoListing(rect3, OptionsMaker, ref scrollPosition, ref viewHeight);
            //Widgets.BeginGroup(rect);
            //Widgets.EndGroup();
            // 下面全是是委托
            List<FloatMenuOption> OptionsMaker()
            {
                List<FloatMenuOption> opts = new();
                for (int i = 0; i < SelTable.def.AllRecipes.Count; i++)
                {
                    RecipeDef recipe;
                    if (SelTable.def.AllRecipes[i].AvailableNow && SelTable.def.AllRecipes[i].AvailableOnNow(SelTable))
                    {
                        recipe = SelTable.def.AllRecipes[i];
                        Add();
                        // 文化物品风格 没啥用
                        /*foreach (Ideo allIdeo in Faction.OfPlayer.ideos.AllIdeos)
                        {
                            foreach (Precept_Building cachedPossibleBuilding in allIdeo.cachedPossibleBuildings)
                            {
                                if (cachedPossibleBuilding.ThingDef == recipe.ProducedThingDef)
                                {
                                    Add(cachedPossibleBuilding);
                                }
                            }
                        }*/
                    }
                    void Add(Precept_ThingStyle precept = null)
                    {
                        string label = ((precept != null) ? "RecipeMake".Translate(precept.LabelCap).CapitalizeFirst() : recipe.LabelCap);
                        opts.Add(new FloatMenuOption(label, delegate
                        {
                            if (ModsConfig.BiotechActive && recipe.mechanitorOnlyRecipe && !SelTable.Map.mapPawns.FreeColonists.Any(MechanitorUtility.IsMechanitor))
                            {
                                Find.WindowStack.Add(new Dialog_MessageBox("RecipeRequiresMechanitor".Translate(recipe.LabelCap)));
                            }
                            else if (!SelTable.Map.mapPawns.FreeColonists.Any((Pawn col) => recipe.PawnSatisfiesSkillRequirements(col)))
                            {
                                Bill.CreateNoPawnsWithSkillDialog(recipe);
                            }
                            Bill bill2 = recipe.MakeNewBill(precept);
                            if (bill2 is Bill_Production)
                            {
                                bill2 = new Bill_ProductionSingle(recipe, precept);
                            }
                            SelTable.billStack.AddBill(bill2);
                            if (recipe.conceptLearned != null)
                            {
                                PlayerKnowledgeDatabase.KnowledgeDemonstrated(recipe.conceptLearned, KnowledgeAmount.Total);
                            }
                            /*if (TutorSystem.TutorialMode)
                            {
                                TutorSystem.Notify_Event("AddBill-" + recipe.LabelCap.Resolve());
                            }*/
                        },
                        #if ver16
                        iconTex: recipe.UIIcon, shownItemForIcon: recipe.UIIconThing, thingStyle: null, forceBasicStyle: false, priority: MenuOptionPriority.Default, mouseoverGuiAction: delegate (Rect rect)
                        #endif
                        #if !ver16
                        itemIcon: recipe.UIIcon, shownItemForIcon: recipe.UIIconThing, thingStyle: null, forceBasicStyle: false, priority: MenuOptionPriority.Default, mouseoverGuiAction: delegate (Rect rect)
                        #endif
                        {
                            BillUtility.DoBillInfoWindow(i, label, rect, recipe);
                        }, 
                        revalidateClickTarget: null, extraPartWidth: 29f, extraPartOnGUI: (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, recipe, precept), revalidateWorldClickTarget: null, playSelectionSound: true, orderInPriority: -recipe.displayPriority));
                    }
                }
                //Finalizer
                if (!SelTable.canProduceNow)
                {
                    opts = new List<FloatMenuOption>
                    {
                        new("LOF_LimitedCraft".Translate(), null)// 未激活
                    };
                }
                else if (!opts.Any()) 
                {
                    opts.Add(new FloatMenuOption("NoneBrackets".Translate(), null));// 无配方
                }
                return opts;
            }
        }

        public override void TabUpdate()
        {
            if (mouseoverBill != null)
            {
                mouseoverBill.TryDrawIngredientSearchRadiusOnMap(SelTable.Position);
                mouseoverBill = null;
            }
        }
    }
}
