using UnityEngine;

public class Default_block : Block
{
    private Renderer rend;
    //Scheiss auf Konstruktor eh Für Looser
    private void Awake()
    {
        // Renderer einmal holen, sobald das Objekt existiert
        rend = GetComponent<Renderer>();
    }

    public override void generate()
    {
        block = gameObject;
        block.transform.localScale = new Vector3(1f, 0.5f, 1f);


        // Grün setzen (sichtbare Farbe)
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material.color = Color.green;
        }
        else
        {
            Debug.LogWarning("[Default_block] Kein Renderer gefunden!");
        }
    }
}