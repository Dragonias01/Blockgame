using UnityEngine;

/// <summary>
/// Verantwortlich fuer das Spawnen des Spielers.
/// BUGFIX: Erbt nicht mehr von Main (MonoBehaviour-Vererbung war falsch,
/// da Spawn_player nie als Komponente an einem GameObject haengt,
/// sondern per "new Spawn_player()" instanziiert wird).
/// </summary>
public class Spawn_player
{
    private FollowPlayer cameraScript;

    public void SpawnPlayer(Vector3 playerSize)
    {
        cameraScript = Object.FindObjectOfType<FollowPlayer>();

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.localScale = playerSize;
        player.transform.position = new Vector3(0, 1, 0);
        player.tag = "Player";

        // Farbe setzen
        Renderer rend = player.GetComponent<Renderer>();
        if (rend != null)
        {
            // Color.lavenderBlush existiert nicht in Unity — BUGFIX: naechste gueltige Farbe
            rend.material.color = new Color(1f, 0.94f, 0.96f); // Lavender Blush RGB
        }
        else
        {
            Debug.LogWarning("[Spawn_player] Kein Renderer gefunden!");
        }

        player.AddComponent<Rigidbody>();
        player.AddComponent<PlayerHandler>();

        if (cameraScript != null)
        {
            cameraScript.cameraTarget = player.transform;
        }
        else
        {
            Debug.LogError("Kein FollowPlayer Script in der Szene gefunden!");
        }
    }
}