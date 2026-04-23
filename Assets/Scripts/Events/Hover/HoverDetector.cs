using UnityEngine;

public class HoverDetector : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isHovering = false;
    private InRange inRange;
    private Main main;

    public void Update()
    {
        if (gameObject.GetComponent<InRange>() == null)
        {
            gameObject.AddComponent<InRange>();
            inRange = GetComponent<InRange>();
        }
    }


    private void OnMouseEnter()
    {
        inRange = GetComponent<InRange>();

        checkshape();

        if (inRange != null && inRange.IsPlayerInRange() && !isHovering)
        {
            transform.localScale = originalScale * 1.1f;
            isHovering = true;
        }
    }

    private void OnMouseExit()
    {
        if (originalScale != null)
        {
            gameObject.transform.localScale = originalScale; // Größe zurücksetzen
            isHovering = false;
        }
    }

    private void checkshape()
    {
        main = FindObjectOfType<Main>();

        if (GetComponent<Tree>() != null)
        {
            originalScale = main.GetTreeSize(); // Originalgröße speichern
        }
        else if (GetComponent<Rock>() != null)
        {
            originalScale = main.GetRockSize(); // Originalgröße speichern
        }
        else
        {
            Debug.LogWarning($"[HoverDetector] Unbekannter Block-Typ auf '{gameObject.name}'");
            originalScale = Vector3.one; // Fallback-Originalgröße
        }
    }
}