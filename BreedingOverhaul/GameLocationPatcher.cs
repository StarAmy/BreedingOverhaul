using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using xTile.Dimensions;
using xTile.ObjectModel;
using System.Reflection;

namespace BreedingOverhaul
{
    internal class GameLocationPatcher
    {
        public static IMonitor monitor;
        public GameLocationPatcher(IMonitor m)
        {
            monitor = m;
        }
        
        /*********
        ** Public methods
        *********/
        /// <inheritdoc />
        public void Apply(Harmony harmony)
        {
            harmony.Patch(
                original: this.RequireMethod<GameLocation>(nameof(GameLocation.performAction)),
                prefix: this.GetHarmonyMethod(nameof(Before_PerformAction), priority: Priority.High)
            );

            monitor.Log("GameLocationPatcher applied", LogLevel.Debug);

        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method to call before <see cref="GameLocation.performAction"/>.</summary>
        private static bool Before_PerformAction(AnimalHouse __instance, string action, Farmer who, Location tileLocation)
        {
            monitor.Log($"Before PerformAction in Breeding Overhaul!! ${action}", LogLevel.Debug);
            return true;
        }


        public HarmonyMethod GetHarmonyMethod(string name, int? priority = null, string before = null)
        {
            var method = new HarmonyMethod(
                AccessTools.Method(this.GetType(), name)
                ?? throw new InvalidOperationException($"Can't find patcher method {GetMethodString(this.GetType(), name)}.")
            );

            if (priority.HasValue)
                method.priority = priority.Value;

            if (before != null)
                method.before = new[] { before };

            return method;
        }

        public static string GetMethodString(Type type, string name, Type[] parameters = null, Type[] generics = null)
        {
            StringBuilder str = new();

            // type
            str.Append(type.FullName);

            // method name (if not constructor)
            if (name != null)
            {
                str.Append('.');
                str.Append(name);
            }

            // generics
            if (generics?.Any() == true)
            {
                str.Append('<');
                str.Append(string.Join(", ", generics.Select(p => p.FullName)));
                str.Append('>');
            }

            // parameters
            if (parameters?.Any() == true)
            {
                str.Append('(');
                str.Append(string.Join(", ", parameters.Select(p => p.FullName)));
                str.Append(')');
            }

            return str.ToString();
        }

        public MethodInfo RequireMethod<TTarget>(string name, Type[] parameters = null, Type[] generics = null)
        {
            return
                AccessTools.Method(typeof(TTarget), name, parameters, generics)
                ?? throw new InvalidOperationException($"Can't find method {GetMethodString(typeof(TTarget), name, parameters, generics)} to patch.");
        }
    }

}
