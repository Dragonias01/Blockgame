using UnityEngine;
using UnityEngine.Events;


public class OnClick : MonoBehaviour
{

    private InRange inRange;

    private void OnMouseDown()
    {
        inRange = GetComponent<InRange>();
        if (GetComponent<Tree>() != null)
        {
            if (inRange != null && inRange.IsPlayerInRange())
            {
                Destroy(gameObject);
            }

        }
        else if (GetComponent<Rock>() != null)
        {
            if (inRange != null && inRange.IsPlayerInRange())
            {
                Destroy(gameObject);
            }

        }
        else if (GetComponent<Block>() != null)
        {

            Debug.Log("Block Angeklickt: " + gameObject.name);
        }
        //Debug.Log("angeklickt: " + gameObject.name);
    }
}