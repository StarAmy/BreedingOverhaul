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
        }*/
        public static bool isEggType(Item dropInItem, int unused, ref bool __result)
        {
            if (dropInItem != null && dropInItem.Name.Contains("Fertilized"))
            {
                __result = true;
                return false;
            }
            return true;
        }
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

        public static bool performObjectDropInActionPatch(StardewValley.Object __instance, Item dropInItem, bool probe, Farmer who, ref bool __result)
        {
            ModEntry.MyMonitor.Log($"object drop in action patch! on {__instance.Name}", LogLevel.Debug);
            if (dropInItem is StardewValley.Object)
            {
                StardewValley.Object dropIn = dropInItem as StardewValley.Object;
                ModEntry.MyMonitor.Log($"object drop in action patch! {dropIn.Name}", LogLevel.Debug);
                
                if (__instance.Name.Contains("Incubator"))
                {

                    //Game1.addHUDMessage(new HUDMessage("The incubator is currently offline", ""));
                    //__result = false;
                    //return false;
                }
            }
            return true;
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
