using UnityEngine;
public class PlayerHandler : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private float playerSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime * -1);
        //spieler bei zu tiefem fall zurücksetzen
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, 3, 0);
            rb.linearVelocity = Vector3.zero;
        }
    }

}