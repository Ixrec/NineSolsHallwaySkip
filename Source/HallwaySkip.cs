using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NineSolsAPI;
using UnityEngine;

namespace HallwaySkip;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class HallwaySkip : BaseUnityPlugin {
    // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/4_configuration.html
    private static ConfigEntry<KeyboardShortcut> skipKeybind = null!;

    public static string SkipKeybindText() {
        return skipKeybind.Value.Serialize();
    }

    private Harmony harmony = null!;

    private void Awake() {
        Log.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);

        // Load patches from any class annotated with @HarmonyPatch
        harmony = Harmony.CreateAndPatchAll(typeof(HallwaySkip).Assembly);

        skipKeybind = Config.Bind("", "Skip Keybind",
            new KeyboardShortcut(KeyCode.K, KeyCode.LeftControl), "The keyboard shortcut to actually skip the hallway.");

        KeybindManager.Add(this, SkipHallwayCutscene, () => skipKeybind.Value);

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        TextDisplay.Awake();
    }

    public static SimpleCutsceneManager activeHallwayCutscene = null;

    private void SkipHallwayCutscene() {
        if (activeHallwayCutscene != null) {
            Log.Info($"calling TrySkip() on {activeHallwayCutscene.name}");
            AccessTools.Method(typeof(SimpleCutsceneManager), "TrySkip").Invoke(activeHallwayCutscene, []);
            activeHallwayCutscene = null;
            TextDisplay.TextComponent.text = null;
        }
    }

    private void OnDestroy() {
        // Make sure to clean up resources here to support hot reloading

        harmony.UnpatchSelf();
        TextDisplay.OnDestroy();
    }
}