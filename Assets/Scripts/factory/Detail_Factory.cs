using UnityEngine;

public class Detail_factory : Factory
{
    public Detail_factory() : base() { }

    public override Object Generate(string type)
    {
        GameObject go = null;

        switch (type)
        {
            case "tree":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Tree";
                go.GetComponent<Collider>().enabled = true;
                go.AddComponent<OnClick>();
                go.AddComponent<HoverDetector>(); // BUGFIX: doppeltes AddComponent entfernt
                return go.AddComponent<Tree>();

            case "rock":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.name = "Rock";
                go.GetComponent<Collider>().enabled = true;
                go.AddComponent<OnClick>();
                go.AddComponent<HoverDetector>();
                return go.AddComponent<Rock>();

            default:
                Debug.LogWarning($"[Detail_factory] Unbekannter Detail-Typ: '{type}'");
                return null;
        }
    }
}
