using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public Transform cameraTarget;
    [SerializeField] private Vector3 offset = new Vector3(5f, 5f, 10f);
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (cameraTarget == null)
        {
            Debug.LogError("Camera target not set for FollowPlayer script.");
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = cameraTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(cameraTarget);
    }

}
