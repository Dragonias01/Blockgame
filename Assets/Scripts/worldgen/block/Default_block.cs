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


        //Material auf die textur Grass1 setzen ein bild was in Assets/Resources/Materials/grass1 liegt
        Material mat = Resources.Load<Material>("Materials/grass1");
        if (mat != null)
        {
            rend.material = mat;
        }
        else
        {
            Debug.LogWarning("[Default_block] Material 'grass1' nicht gefunden!");
        }

    }
}