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
using StardewValley.Objects;

namespace BreedingOverhaul
{
    [HarmonyPatch(typeof(StardewValley.AnimalHouse))]
    internal class IncubatorPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(StardewValley.AnimalHouse.incubator))]
        public static bool incubator()
        {
            //ModEntry.MyMonitor.Log($"Overridden incubator function");
            Game1.addHUDMessage(new HUDMessage("The incubator is currently offline", ""));
            return true;
        }
    }
}
