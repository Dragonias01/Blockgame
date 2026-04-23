using UnityEngine;

public class WaterPlane : Block
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
        block.transform.localScale = new Vector3(100f, 0.25f, 100f);


        // Grün setzen (sichtbare Farbe)
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material.color = Color.blue;
        }
        else
        {
            Debug.LogWarning("[WaterPlane] Kein Renderer gefunden!");
        }
    }
}