using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    private UI_Factory factory;

    private void Start()
    {
        factory = new UI_Factory();
        CreateUI();
    }

    private void CreateUI()
    {
        // Erstelle ein Canvas-Element
        UI_Canvas canvas = factory.Generate("canvas") as UI_Canvas;
        if (canvas != null)
        {
            canvas.CreateElement();
            canvas.GetElement().transform.SetParent(this.transform);
        }
        // Erstelle ein Text-Element
        UI_Text text = factory.Generate("text") as UI_Text;
        if (text != null)
        {
            text.CreateElement();
            text.GetElement().transform.SetParent(this.transform);
        }

    }
}