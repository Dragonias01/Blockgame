using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private void Start()
    {
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject buttonObj = new GameObject("Button");
        buttonObj.transform.SetParent(canvasObj.transform);

        Button button = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();

        image.color = Color.red;

        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, 80);
        rt.anchoredPosition = Vector2.zero;
    }
}