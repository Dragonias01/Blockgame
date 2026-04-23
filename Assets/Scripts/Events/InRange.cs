using UnityEngine;

public class InRange : MonoBehaviour
{
    private Transform playerTransform;
    private Main main;

    private void Start()
    {
        main = FindObjectOfType<Main>();
        if (main == null)
        {
            Debug.LogError("Main Script nicht gefunden!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Kein GameObject mit dem Tag 'Player' gefunden!");
        }
    }

    public bool IsPlayerInRange()
    {
        if (playerTransform == null || main == null) return false;

        float range = main.GetRange();
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        return distance <= range;
    }
}