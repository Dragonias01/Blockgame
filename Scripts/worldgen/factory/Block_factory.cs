using UnityEngine;

public class Block_factory : Factory
{
    public Block_factory() : base() { }
    public override Block Generate(string type)
    {
        switch (type)
        {
            case "default1":
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Default_block";
                return go.AddComponent<Default_block>();

            default:
                Debug.LogWarning($"[Block_factory] Unbekannter Block-Typ: '{type}'");
                return null;
        }
    }
}