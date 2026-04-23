using UnityEngine;
using UnityEngine.Events;


public class OnClick : MonoBehaviour
{

    private InRange inRange;
    private Main main;

    private void OnMouseDown()
    {
        inRange = GetComponent<InRange>();
        if (GetComponent<Tree>() != null)
        {
            if (inRange != null && inRange.IsPlayerInRange())
            {
                Destroy(gameObject);
                // Update Holzbestand in Mainklasse
                main = FindObjectOfType<Main>();
                if (main != null)
                {
                    int currentWood = main.GetDcWood();
                    main.SetDcWood(currentWood + 1);
                    Debug.Log("Holz gesammelt! Aktueller Holzbestand: " + main.GetDcWood());
                }
            }

        }
        else if (GetComponent<Rock>() != null)
        {
            if (inRange != null && inRange.IsPlayerInRange())
            {
                Destroy(gameObject);
                // Update Steinbestand in Mainklasse
                main = FindObjectOfType<Main>();
                if (main != null)
                {
                    int currentStone = main.GetDcStone();
                    main.SetDcStone(currentStone + 1);
                    Debug.Log("Stein gesammelt! Aktueller Steinbestand: " + main.GetDcStone());
                }
            }

        }
        else if (GetComponent<Block>() != null)
        {

            Debug.Log("Block Angeklickt: " + gameObject.name);
        }
        //Debug.Log("angeklickt: " + gameObject.name);
    }
}