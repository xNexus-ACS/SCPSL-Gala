using HarmonyLib;
using InventorySystem.Items.Firearms.Modules;
using UnityEngine;

namespace GalaPlugin.Patches
{
    /// <summary>
    /// Para Prevenir BulletHoles al disparar con un arma durante la gala, se desactiva con una configuracion
    /// </summary>
    [HarmonyPatch(typeof(StandardHitregBase), nameof(StandardHitregBase.PlaceBulletholeDecal))]
    internal static class BulletHolePatch
    {
        private static bool Prefix(StandardHitregBase __instance, Ray ray, RaycastHit hit)
        {
            if (!MainClass.Singleton.Config.BulletHolesAllowed)
                return false;
            return true;
        }
    }
}