using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private int bounds = 5;
    [SerializeField] private int spacing = 10;
    private Worldgen worldgen;
    void Start()
    {
        worldgen = new Worldgen();
        worldgen.GenerateWorld(bounds, spacing);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
