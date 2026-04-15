using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerHandler ph = other.GetComponent<PlayerHandler>();
        if (ph != null)
        {
            ph.IncresePoints();
            Destroy(this.gameObject);
        }
    }

}
