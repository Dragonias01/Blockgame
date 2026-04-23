using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public Transform cameraTarget;

    private Vector3 roffset;
    private float offset;
    private float smoothTime;
    private Main main;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        main = FindObjectOfType<Main>();
        if (main != null)
        {
            offset = main.GetOffset();
            roffset = new Vector3(offset * 0.5f, offset * 0.5f, offset);
            smoothTime = main.GetSmoothTime();
        }

        if (cameraTarget == null)
        {
            Debug.LogError("Camera target not set for FollowPlayer script.");
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = cameraTarget.position + roffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(cameraTarget);
    }

}
