using UnityEngine;
public class Rock : Block
{
    private Renderer rend;

    private void Awake()
    {
        // Renderer einmal holen, sobald das Objekt existiert
        rend = GetComponent<Renderer>();
    }

    public override void generate()
    {
        block = gameObject;
        block.transform.localScale = new Vector3(1f, 0.125f, 1f);


        // Blau setzen (sichtbare Farbe)
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material.color = Color.gray;
        }
        else
        {
            Debug.LogWarning("[Rock] Kein Renderer gefunden!");
        }
    }
}