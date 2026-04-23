using UnityEngine;
public class UI_Canvas : UI_Element
{
    public override void CreateElement()
    {
        element = new GameObject("Canvas");
        Canvas canvas = element.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Füge die benötigten Komponenten hinzu
        //element.AddComponent<CanvasScaler>();
        //element.AddComponent<GraphicRaycaster>();
    }
}