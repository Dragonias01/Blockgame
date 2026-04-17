using UnityEngine;
public class Detail_factory : Factory
{
    public Detail_factory() : base() { }
    public override Block Generate(string type)
    {
        switch (type)
        {
            case "detail1":
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Detail1";
                return go.AddComponent<Detail1>();

            default:
                Debug.LogWarning($"[Detail_factory] Unbekannter Detail-Typ: '{type}'");
                return null;
        }
    }
}