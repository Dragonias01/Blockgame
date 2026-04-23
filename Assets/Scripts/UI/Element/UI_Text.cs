using UnityEngine;
using UnityEngine.UI;
public class UI_Text : UI_Element
{
    public override void CreateElement()
    {
        element = new GameObject("Text");
        Text textComponent = element.AddComponent<Text>();
        textComponent.text = "Hello, World!";
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.color = Color.black;
    }
}