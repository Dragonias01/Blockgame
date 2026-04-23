using UnityEngine;

public class HoverDetector : MonoBehaviour
{
    private Vector3 originalScale;
    private void OnMouseEnter()
    {
        //Debug.Log("Hover Start: " + gameObject.name);
        //scale erhalten
        originalScale = gameObject.transform.localScale;
        gameObject.transform.localScale = originalScale * 1.1f; // Größe vergrößern

    }

    private void OnMouseExit()
    {
        //Debug.Log("Hover Ende: " + gameObject.name);
        gameObject.transform.localScale = originalScale; // Größe zurücksetzen
    }
}