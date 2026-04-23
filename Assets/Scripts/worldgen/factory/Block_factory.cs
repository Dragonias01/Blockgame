using UnityEngine;

public class Block_factory : Factory
{
    public Block_factory() : base() { }
    public override Block Generate(string type)
    {
        GameObject go = null;
        switch (type)
        {
            case "default1":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Default_block";
                go.AddComponent<BoxCollider>(); // Collider hinzufügen
                go.AddComponent<OnClick>(); // Klick-Handler hinzufügen
                return go.AddComponent<Default_block>();
            case "water":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "WaterPlane";
                go.GetComponent<Collider>().enabled = false;
                return go.AddComponent<WaterPlane>();
            default:
                Debug.LogWarning($"[Block_factory] Unbekannter Block-Typ: '{type}'");
                return null;
        }
    }
}