using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using HarmonyLib;
using System.Reflection;
using StardewValley.Menus;
using System.Collections.Generic;
using System.Linq;
using xTile;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;using xTile.Layers;
using xTile.Tiles;
using StardewValley.Objects;
using StardewValley.Buildings;

namespace BreedingOverhaul
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        //private HarmonyLib.Harmony Harmony;
        public static IModHelper MyHelper;
        public static IMonitor MyMonitor;
        public static HarmonyLib.Harmony harmony;
        private static IncubatorPatch ipatch;
        private static GameLocationPatcher glp;
        public static IncubatorData incubatorData;

        //AnimalBuildingData = DataLoader.Helper.Data.ReadJsonFile<AnimalBuildingData>("data\\animalBuilding.json")

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Monitor.Log($"Mod entry in Breeeding Overhaul.", LogLevel.Debug);
            Harmony.DEBUG = true;
            harmony = new HarmonyLib.Harmony("StarAmy.BreedingOverhaul");
            MyHelper = helper;
            MyMonitor = this.Monitor;
            ipatch = new IncubatorPatch();

            incubatorData = MyHelper.Data.ReadJsonFile<IncubatorData>("data\\incubatordata.json") ?? null;
            if (incubatorData == null)
            {
                this.Monitor.Log($"No incubator data file.", LogLevel.Debug);
            } else
            {
                this.Monitor.Log($"Incubator data file loaded.", LogLevel.Debug);
            }

            //glp = new GameLocationPatcher(this.Monitor);
            //glp.Apply(harmony);

            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }


        /*********
        ** Private methods
        *********/
        private void aboutPatch(Type typ, string method)
        {
            MyMonitor.Log($"Checking {typ.Name} {method} patches", LogLevel.Debug);

            // get the MethodBase of the original
            var original =typ.GetMethod(method);

            // retrieve all patches
            var patches = Harmony.GetPatchInfo(original);
            if (patches is null)
            {
                MyMonitor.Log($"{method} not patched", LogLevel.Debug);
            }
            else
            {
                // get a summary of all different Harmony ids involved
                MyMonitor.Log("all owners: " + patches.Owners, LogLevel.Debug);

                // get info about all Prefixes/Postfixes/Transpilers
                foreach (var patch in patches.Prefixes)
                {
                    MyMonitor.Log("index: " + patch.index, LogLevel.Debug);
                    MyMonitor.Log("owner: " + patch.owner, LogLevel.Debug);
                    MyMonitor.Log("patch method: " + patch.PatchMethod, LogLevel.Debug);
                    MyMonitor.Log("priority: " + patch.priority, LogLevel.Debug);
                    MyMonitor.Log("before: " + patch.before, LogLevel.Debug);
                    MyMonitor.Log("after: " + patch.after, LogLevel.Debug);
                }
            }
        }

        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
/*
            if (e.Button.ToString() == "P")
            {
                this.applyPatches();
            } else if (e.Button.ToString() == "L")
            {
                glp.Apply(harmony);
            }*/
        }

        private void applyPatches()
        {
            aboutPatch(typeof(AnimalHouse), "incubator");
            aboutPatch(typeof(GameLocation), "performAction");
            aboutPatch(typeof(AnimalHouse), "addNewHatchedAnimal");

            MyMonitor.Log($"Applying all patches", LogLevel.Debug);
            harmony.Patch(original: AccessTools.Method(typeof(StardewValley.AnimalHouse), nameof(StardewValley.AnimalHouse.isFull)),
              prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.isFullPatch)));
            //harmony.Patch(original: AccessTools.Method(typeof(StardewValley.AnimalHouse), nameof(StardewValley.AnimalHouse.checkAction)),
            //              prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.checkActionPatch)));
            harmony.Patch(original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.performObjectDropInAction)),
                          prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.performObjectDropInActionPatch)));
            // all patches from that other Harmony user:
            /*
            MyMonitor.Log($"Patching addNewHatchedAnimal - removing prefixes", LogLevel.Debug);
            var og = typeof(StardewValley.AnimalHouse).GetMethod("addNewHatchedAnimal");
            var ogPatches = Harmony.GetPatchInfo(og);
            harmony.Unpatch(og, HarmonyPatchType.All, "Paritee.BetterFarmAnimalVariety");
            HarmonyMethod hm = new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.addNewHatchedAnimalPatch));
            hm.priority = Priority.First;
            harmony.Patch(original: AccessTools.Method(typeof(StardewValley.AnimalHouse), nameof(StardewValley.AnimalHouse.addNewHatchedAnimal)),
                         prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.addNewHatchedAnimalPatch)));
            foreach (var patch in ogPatches.Prefixes)
            {
                if (patch.owner.Contains("StarAmy"))
                {
                    continue;
                }
                MyMonitor.Log($"Patching back in ${patch.owner} ${patch.PatchMethod}");
                harmony.Patch(og, prefix: new HarmonyMethod(patch.PatchMethod));
            }
            aboutPatch(typeof(AnimalHouse), "addNewHatchedAnimal");*/

            harmony.Patch(original: AccessTools.Method( typeof(Paritee.StardewValley.Core.Locations.AnimalHouse), nameof(Paritee.StardewValley.Core.Locations.AnimalHouse.GetRandomTypeFromIncubator)),
                prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.getRandomTypePatch)));


            //harmony.Patch(original: AccessTools.Method(typeof(StardewValley.Buildings.Coop), nameof(StardewValley.Buildings.Coop.doAction)),
            //             prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.doActionPatch)));
            harmony.Patch(original: AccessTools.Method(typeof(StardewValley.AnimalHouse), "resetSharedState"),
                         prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.resetSharedStatePatch)));

            //harmony.Patch(original: AccessTools.Method(typeof(StardewValley.AnimalHouse), nameof(StardewValley.AnimalHouse.incubator)),
            //   prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.incubator)));
            
        }

        /// <summary>Raised after the game is launched, right before the first update tick. This happens once per game session (unrelated to loading saves). All mods are loaded and initialised at this point, so this is a good time to set up mod integrations.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event data.</param>
        private void OnGameLaunched(object sender, GameLaunchedEventArgs args)
        {
            this.Monitor.Log($"Patching in Breeeding Overhaul for {MyHelper.ModRegistry.ModID}.", LogLevel.Debug);
            applyPatches();

            harmony.Patch(original: AccessTools.Method(typeof(Tool), nameof(Tool.beginUsing)),
                          prefix: new HarmonyMethod(typeof(IncubatorPatch), nameof(IncubatorPatch.beginUsingPatch)));

        }
    }
}