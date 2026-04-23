using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Worldgen Einstellungen")]
    [SerializeField] private float bounds = 5f;
    [SerializeField] private float spacing = 1.1f;
    [SerializeField] private int Detailspread = 20;
    [Header("Spieler Einstellungen")]
    [SerializeField] private Vector3 player_size = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private float range = 2f;
    [Header("Form Einstellungen")]
    [SerializeField] private Vector3 tree_size = new Vector3(0.25f, 1f, 0.25f);
    [SerializeField] private Vector3 rock_size = new Vector3(0.5f, 0.5f, 0.5f);


    //Klassen
    private Worldgen worldgen;
    private Spawn_player spawnPlayer;
    private Camera mainCamera;

    void Start()
    {
        // setup Camera
        mainCamera = Camera.main;
        //Component zuweisung:
        if (mainCamera != null)
        {
            //Component der Main Camera hinzufügen, damit sie dem Spieler folgt
            mainCamera.gameObject.AddComponent<FollowPlayer>();


        }
        else
        {
            Debug.LogError("Keine Main Camera gefunden!");
        }

        // setup Worldgen
        worldgen = new Worldgen();
        worldgen.GenerateWorld(bounds, spacing, Detailspread);

        // setup Player
        spawnPlayer = new Spawn_player();
        spawnPlayer.SpawnPlayer(player_size);
    }

    //Eigenschafts Methode für die Reichweite
    public float GetRange() { return range; }
    public Vector3 GetTreeSize() { return tree_size; }
    public Vector3 GetRockSize() { return rock_size; }
}