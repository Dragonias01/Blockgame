using UnityEngine;
public class Detail_factory : Factory
{
    GameObject go = null;

    public Detail_factory() : base() { }
    public override Object Generate(string type)
    {
        switch (type)
        {
            case "tree":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Tree";

                go.GetComponent<Collider>().enabled = true;
                go.AddComponent<OnClick>(); // Klick-Handler hinzufügen
                go.AddComponent<HoverDetector>(); // Hover-Handler hinzufügen
                go.AddComponent<HoverDetector>();

                return go.AddComponent<Tree>();
            case "rock":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Rock";

                go.GetComponent<Collider>().enabled = true;
                go.AddComponent<OnClick>(); // Klick-Handler hinzufügen
                go.AddComponent<HoverDetector>(); // Hover-Handler hinzufügen

                return go.AddComponent<Rock>();
            default:
                Debug.LogWarning($"[Detail_factory] Unbekannter Detail-Typ: '{type}'");
                return null;
        }
    }
}