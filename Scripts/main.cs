using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private float bounds = 5f;
    [SerializeField] private float spacing = 1.1f;

    [SerializeField] private Vector3 player_size = new Vector3(0.5f, 0.5f, 0.5f);
    private Worldgen worldgen;
    private Spawn_player spawnPlayer;
    void Start()
    {
        //setup Worldgen
        worldgen = new Worldgen();
        worldgen.GenerateWorld(bounds, spacing);
        //setup Player
        spawnPlayer = new Spawn_player();
        spawnPlayer.SpawnPlayer(player_size);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
