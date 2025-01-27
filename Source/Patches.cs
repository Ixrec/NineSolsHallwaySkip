using HarmonyLib;
using NineSolsAPI;
using System.Collections.Generic;
using UnityEngine;

namespace HallwaySkip;

[HarmonyPatch]
public class Patches {
    private static string hallwayCutsceneGOPath = "A11_S2/Room/Prefab/EventBinder/OldBoy FSM Object/FSM Animator/LogicRoot/[CutScene]OldBoyFighting/[Timeline]";

    [HarmonyPrefix, HarmonyPatch(typeof(SimpleCutsceneManager), "PlayAnimation")]
    private static void SimpleCutsceneManager_PlayAnimation(SimpleCutsceneManager __instance) {
        var hallwayGO = GameObject.Find(hallwayCutsceneGOPath);
        if (hallwayGO != null && hallwayGO == __instance.gameObject) {
            Notifications.AddNotification($"Press {HallwaySkip.SkipKeybindText()} to Skip The Hallway Fight");
        }

        HallwaySkip.activeCutscene = __instance;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(SimpleCutsceneManager), "End")]
    private static void SimpleCutsceneManager_End(SimpleCutsceneManager __instance) {
        Log.Debug($"SimpleCutsceneManager_End {__instance.name}");
        if (HallwaySkip.activeCutscene == __instance)
            HallwaySkip.activeCutscene = null;
    }
}