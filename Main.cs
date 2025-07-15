using System.Collections;
using Silk;
using Logger = Silk.Logger; // Alias for Silk.Logger to Logger
using HarmonyLib; // Library for runtime method patching
using UnityEngine;

namespace TemplateMod
{
    // SilkMod Attribute with with the format: name, authors, mod version, silk version, mod identifier, and networking type
    [SilkMod("Silk Mod", new[] { "Abstractmelon" }, "1.0.0", "0.6.0", "silk-mod", 1)]
    public class TemplateMod : SilkMod
    {
        private float timer = 0; // Timer variable to track time 

        // Called by Silk when Unity loads this mod
        public override void Initialize()
        {
            // Log mod started
            Logger.LogInfo("Initializing Silk Mod...");

            // Create and apply Harmony patches
            Harmony harmony = new Harmony("com.Abstractmelon.SilkMod"); // Create a Harmony instance for patching
            harmony.PatchAll(); // Apply all Harmony patches

            // Log mod finished
            Logger.LogInfo("Harmony patches applied.");
        }

        // Called by Unity when the script instance is being loaded
        public void Awake()
        {
            Logger.LogInfo("Awake called.");
        }

        // Called every frame by Unity
        public void Update()
        {
            Logger.LogInfo("Update called.");
            timer += Time.deltaTime; // Increment timer by the time elapsed since the last frame
            Logger.LogInfo($"Timer updated: {timer}");

            // Check if the timer has exceeded 1 second
            if (timer > 1)
            {
                Logger.LogInfo("Timer exceeded 1 second, resetting timer.");
                timer = 0; // Reset the timer

                // Find all instances of EnemyHealthSystem in the scene
                EnemyHealthSystem[] array = FindObjectsOfType<EnemyHealthSystem>();
                Logger.LogInfo($"Found {array.Length} enemies.");

                // Loop through each enemy and call their Disintegrate method
                for (int i = 0; i < array.Length; i++)
                {
                    Logger.LogInfo($"Disintegrating enemy {i + 1}.");
                    array[i].Disintegrate();
                }
            }
        }

        // Called by Silk when the mod is being unloaded, undo what your mod does in `Initialize()`
        public override void Unload()
        {
            Logger.LogInfo("Unloading Silk Mod...");
            Harmony.UnpatchID("com.Abstractmelon.SilkMod");
        }
    }
}

