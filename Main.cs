using Silk;
using Logger = Silk.Logger;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace TemplateMod
{
    [SilkMod("Silk Mod", new[] { "Abstractmelon" }, "1.0.0", "0.6.1", "silk-mod", 1)]
    public class TemplateMod : SilkMod
    {
        public const string ModId = "silk-mod";

        private Harmony _harmony;

        public override void Initialize()
        {
            Logger.LogInfo("Initializing Silk Mod...");

            // Load config with default "enableChristmas" set to true
            var defaultConfig = new Dictionary<string, object>
            {
                { "enableChristmas", true }
            };
            Config.LoadModConfig(ModId, defaultConfig);

            _harmony = new Harmony("com.Abstractmelon.SilkMod");
            _harmony.PatchAll(typeof(Patches));

            Logger.LogInfo("Harmony patches applied.");
        }

        public void Awake()
        {
            Logger.LogInfo("Awake called.");
        }

        public override void Unload()
        {
            Logger.LogInfo("Unloading Silk Mod...");
            _harmony.UnpatchSelf();
        }
    }

    public static class Patches
    {
        [HarmonyPatch(typeof(SeasonChecker), nameof(SeasonChecker.IsItChristmas))]
        [HarmonyPrefix]
        public static bool MakeItChristmas(ref bool __result)
        {
            __result = Config.GetModConfigValue(TemplateMod.ModId, "enableChristmas", true);
            return false;
        }
    }
}
