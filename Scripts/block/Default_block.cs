using UnityEngine;

public class Default_block : Block
{

    public Default_block() : base()
    {
        plattform.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void generate()
    {
        // TODO: Blockdaten laden (z.B. Mesh, Material, Kollision setzen)
        plattform = gameObject;

    }
}
