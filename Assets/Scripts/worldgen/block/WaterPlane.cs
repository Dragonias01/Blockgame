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

        //Material auf die textur Wassertextur setzen ein bild was in Assets/Resources/Materials/wasser1 liegt
        Material mat = Resources.Load<Material>("Materials/wasser1");
        if (mat != null)
        {
            rend.material = mat;
        }
        else
        {
            Debug.LogWarning("[WaterPlane] Material 'wasser1' nicht gefunden!");
        }

    }
}