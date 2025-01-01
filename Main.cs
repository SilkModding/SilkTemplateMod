using Silk;
using Logger = Silk.Logger;
using HarmonyLib;
using UnityEngine;

namespace SpiderInvincibility
{
    /// <summary>
    /// The SpiderInvincibility mod prevents spiders from taking any damage.
    /// </summary>
    [SilkMod("Invincibility", new[] { "Abstractmelon" }, "1.0.0", "1.6a", "spider-invincibility")]
    public class SpiderInvincibility : SilkMod
    {
        private Harmony harmonyInstance;

        /// <summary>
        /// Initializes the mod and patches harmony.
        /// </summary>
        public void Initialize()
        {
            Logger.LogInfo("Invincibility mod has loaded!");

            harmonyInstance = new Harmony("com.Abstractmelon.Invincibility");
            harmonyInstance.PatchAll();
        }

        /// <summary>
        /// Called every frame. Currently not used in this mod.
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Unloads the mod. 
        /// </summary>
        public void Unload()
        {   
            harmonyInstance.UnpatchSelf();
            Logger.LogInfo("Invincibility mod has unloaded!");

        }
    }

    /// <summary>
    /// Contains patches for the SpiderHealthSystem to prevent spiders from taking damage.
    /// </summary>
    [HarmonyPatch(typeof(SpiderHealthSystem))]
    public static class SpiderHealthSystemPatches
    {
        /// <summary>
        /// Prevents the spider from taking damage by skipping the original Damage method.
        /// </summary>
        /// <returns>False to skip the original method.</returns>
        [HarmonyPatch(nameof(SpiderHealthSystem.Damage))]
        [HarmonyPrefix]
        public static bool PreventDamage()
        {
            return false;
        }

        /// <summary>
        /// Prevents the spider from disintegrating by skipping the original Disintegrate method.
        /// </summary>
        /// <returns>False to skip the original method.</returns>
        [HarmonyPatch(nameof(SpiderHealthSystem.Disintegrate))]
        [HarmonyPrefix]
        public static bool PreventDisintegration()
        {
            return false;
        }

        /// <summary>
        /// Prevents the spider from exploding in any direction by skipping the original ExplodeInDirection method.
        /// </summary>
        /// <returns>False to skip the original method.</returns>
        [HarmonyPatch(nameof(SpiderHealthSystem.ExplodeInDirection))]
        [HarmonyPrefix]
        public static bool PreventExplosion()
        {
            return false;
        }
    }
}

