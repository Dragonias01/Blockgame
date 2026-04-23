using UnityEngine;
public class Tree : Block
{
    private Renderer rend;
    private Main main;

    private void Awake()
    {
        main = FindObjectOfType<Main>();
        if (main == null)
        {
            Debug.LogError("Main Script nicht gefunden!");
            return;
        }
        // Renderer einmal holen, sobald das Objekt existiert
        rend = GetComponent<Renderer>();
    }

    public override void generate()
    {
        block = gameObject;
        block.transform.localScale = main.GetTreeSize();


        // Blau setzen (sichtbare Farbe)
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material.color = Color.green;
        }
        else
        {
            Debug.LogWarning("[Tree] Kein Renderer gefunden!");
        }
    }
}