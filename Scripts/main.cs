using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private float bounds = 5f;
    [SerializeField] private float spacing = 1.1f;
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
