using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using StardewValley.Objects;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Buildings;
using xTile.Dimensions;
using StardewModdingAPI;
using System.Reflection;

namespace BreedingOverhaul
{
    public class IncubatorPatch
    {
        /*public static bool incubator()
        {
            ModEntry.MyMonitor.Log($"Overridden incubator function", LogLevel.Debug);
            Game1.addHUDMessage(new HUDMessage("The incubator is currently offline", ""));
            return false;
        }
        public static bool isEggType(Item item, int index, ref bool __result)
        {
            if (item != null && item.Name.Contains("Fertilized"))
            {
                __result = true;
                return false;
            }
            return true;
        }*/
        public static void isFullPatch(ref bool __result)
        {
            //ModEntry.MyMonitor.Log($"AnimalHouse isFull trace {Environment.StackTrace}", LogLevel.Debug);
            //__result = false;
        }

        public static bool beginUsingPatch(GameLocation location, int x, int y, Farmer who, ref bool __result)
        {
            ModEntry.MyMonitor.Log($"I began using a tool!", LogLevel.Debug);
            return true;
        }

        public static bool checkActionPatch(Location tileLocation, xTile.Dimensions.Rectangle viewport, Farmer who, ref bool __result)
        {
            ModEntry.MyMonitor.Log($"Check action patch (via AnimalHouse)!", LogLevel.Debug);
            if (who.ActiveObject != null)
            {
                ModEntry.MyMonitor.Log($"You are holding a {who.ActiveObject.Name}", LogLevel.Debug);
                
            }            
            return true;
        }

        public static bool performObjectDropInActionPrefix(StardewValley.Object __instance, Item dropInItem, bool probe, Farmer who, ref int __state, ref bool __result)
        {
            ModEntry.MyMonitor.Log($"object drop in action patch! on {__instance.Name}, drop in {dropInItem.Name} probe is {probe}", LogLevel.Debug);
            if (__instance.Name.Equals("Incubator") && probe == false)
            {
                __state = new int();
                __state = dropInItem.Category;
                if (ModEntry.incubatorData.IncubatorItems.ContainsKey(dropInItem.Name))
                {
                    ModEntry.MyMonitor.Log($"making {dropInItem.Name} an egg catagory temporarily {__state} -> -5", LogLevel.Debug);
                    dropInItem.Category = -5;
                }
                else if (dropInItem.Category == -5)
                {
                    ModEntry.MyMonitor.Log($"making {dropInItem.Name} NOT an egg temporarily {__state} -> 0", LogLevel.Debug);
                    dropInItem.Category = 0;
                } else
                {
                    ModEntry.MyMonitor.Log($"not in list or an egg", LogLevel.Debug);
                }

            }

            return true;
        }

        public static void performObjectDropInActionPostfix(StardewValley.Object __instance, Item dropInItem, bool probe, Farmer who, ref int __state, ref bool __result)
        {
            if (__instance.Name.Equals("Incubator") && __state == -5 && probe == false)
            {
                ModEntry.MyMonitor.Log($"restoring {dropInItem.Name} category {dropInItem.Category} -> {__state} -> 0", LogLevel.Debug);
                dropInItem.Category = __state;
            } else
            {
                ModEntry.MyMonitor.Log($" no work to be done postfix {dropInItem.Name} category {dropInItem.Category} into {__instance.Name}", LogLevel.Debug);
            }
        }


        public static bool performUseAction(StardewValley.Object __instance, GameLocation location, ref bool __result)
        {
            ModEntry.MyMonitor.Log($"perform use action patch on {__instance.Name}", LogLevel.Debug);
            return true;
        }

        public static bool addNewHatchedAnimalPatch(AnimalHouse __instance, string name)
        {
            ModEntry.MyMonitor.Log($"add new hatched animal name: {name}", LogLevel.Debug);
            return true;
        }

        public static bool doActionPatch(StardewValley.Buildings.Coop __instance, Vector2 tileLocation, Farmer who)
        {
            ModEntry.MyMonitor.Log($"doAction patch", LogLevel.Debug);
            return true;
        }

        public static bool resetSharedStatePatch(AnimalHouse __instance)
        {
            ModEntry.MyMonitor.Log($"resetSharedState patch", LogLevel.Debug);
            return true;
        }

        public static bool getRandomTypePatch(StardewValley.Object incubator, Dictionary<string, List<string>> restrictions, ref string __result)
        {
            // get the egg type
            ModEntry.MyMonitor.Log($"getRandomTypePatch, this is used to return the type of animal based on egg.", LogLevel.Debug);
            if (ModEntry.incubatorData == null)
            {
                ModEntry.MyMonitor.Log($"getRandomTypePatch, no json loaded", LogLevel.Debug);
                return true;
            }
            if (incubator == null)
            {
                ModEntry.MyMonitor.Log($"getRandomTypePatch, no instance of object", LogLevel.Debug);
                return true;
            }
            StardewValley.Object egg = incubator.heldObject.Value;
            if (egg != null)
            {
                ModEntry.MyMonitor.Log($"getRandomTypePatch, input egg is a {egg.Name}, {egg.DisplayName}", LogLevel.Debug);

                if (ModEntry.incubatorData.IncubatorItems.ContainsKey(egg.Name))
                {
                    List<string> animals = ModEntry.incubatorData.IncubatorItems[egg.Name];
                    string animal = animals[new Random().Next(animals.Count)];
                    ModEntry.MyMonitor.Log($"getRandomTypePatch, input egg is a {egg.Name}, random animal is {animal}", LogLevel.Debug);
                    __result = animal;
                    return false;
                } else
                {
                    ModEntry.MyMonitor.Log($"getRandomTypePatch, no config for input egg {egg.Name}", LogLevel.Debug);
                }
            }
            else
            {
                ModEntry.MyMonitor.Log($"no egg in incubator", LogLevel.Debug);
            }
            return true;
        }

    }


}
