using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Worldgen Einstellungen")]
    [SerializeField] private float bounds = 5f;
    [SerializeField] private float spacing = 1.1f;
    [SerializeField] private int Detailspread = 20;

    [SerializeField] private Vector3 player_size = new Vector3(0.5f, 0.5f, 0.5f);

    private Worldgen worldgen;
    private Spawn_player spawnPlayer;
    private Camera mainCamera;

    void Start()
    {
        // setup Worldgen
        worldgen = new Worldgen();
        worldgen.GenerateWorld(bounds, spacing, Detailspread);

        // setup Player
        spawnPlayer = new Spawn_player();
        spawnPlayer.SpawnPlayer(player_size);

        // setup Camera
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            //mainCamera.gameObject.AddComponent<ClickDetector>();
        }
        else
        {
            Debug.LogError("Keine Main Camera gefunden!");
        }
    }
}