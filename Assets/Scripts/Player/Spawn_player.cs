using System.Runtime.CompilerServices;
using UnityEngine;
public class Spawn_player : Main
{
    private FollowPlayer cameraScript;

    public void SpawnPlayer(Vector3 playerSize)
    {
        cameraScript = FindObjectOfType<FollowPlayer>();

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.localScale = playerSize;
        player.transform.position = new Vector3(0, 1, 0);
        //farbe sezten
        Renderer rend = player.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.lavenderBlush;
        }
        else
        {
            Debug.LogWarning("[Spawn_player] Kein Renderer gefunden!");
        }
        player.AddComponent<Rigidbody>();
        player.AddComponent<PlayerHandler>();
        player.tag = "Player";

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