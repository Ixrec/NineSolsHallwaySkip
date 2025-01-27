using UnityEngine;
using TMPro;

namespace HallwaySkip;

/*
 * We don't use NineSolsAPI.ToastManager because it breaks whenever you quit to the main menu,
 * because RCGLifeCycle.DontDestroyForever() doesn't protect child objects. 
 * This class only uses a single GO, so it doesn't have that problem.
 */
internal class TextDisplay {
    private static Canvas CanvasComponent = null!;
    public static TextMeshProUGUI TextComponent = null;

    public static void Awake() {
        var fullscreenCanvasObject = new GameObject("NineSolsAPI-FullscreenCanvas");
        RCGLifeCycle.DontDestroyForever(fullscreenCanvasObject);

        CanvasComponent = fullscreenCanvasObject.AddComponent<Canvas>();
        CanvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;

        TextComponent = fullscreenCanvasObject.AddComponent<TextMeshProUGUI>();
        TextComponent.alignment = TextAlignmentOptions.BottomRight;
        TextComponent.fontSize = 20;
        TextComponent.color = Color.white;
    }
    public static void OnDestroy() {
        UnityEngine.Object.Destroy(CanvasComponent.gameObject);
    }
}
