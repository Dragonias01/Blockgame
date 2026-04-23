using System.Runtime.CompilerServices;
using UnityEngine;
public class Spawn_player : MonoBehaviour
{
    private FollowPlayer cameraScript;

    public void SpawnPlayer(Vector3 playerSize)
    {
        cameraScript = FindObjectOfType<FollowPlayer>();

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.localScale = playerSize;
        player.transform.position = new Vector3(0, 1, 0);

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