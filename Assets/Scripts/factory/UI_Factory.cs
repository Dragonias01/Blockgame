using UnityEngine;
using UnityEngine.UI;
public class UI_Factory : Factory
{
    public override Object Generate(string type)
    {
        GameObject go = null;
        switch (type)
        {
            case "canvas":
                go = new GameObject("Canvas");
                go.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                return go.AddComponent<UI_Canvas>();
            case "text":
                go = new GameObject("Text");
                Text textComponent = go.AddComponent<Text>();
                textComponent.text = "Hello, World!";
                textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                textComponent.color = Color.black;
                return go.AddComponent<UI_Text>();
            default:
                Debug.LogWarning($"[UI_Factory] Unbekannter UI-Typ: '{type}'");
                return null;
        }
    }
}