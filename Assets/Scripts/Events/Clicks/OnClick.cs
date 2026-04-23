using UnityEngine;
using UnityEngine.Events;


public class OnClick : MonoBehaviour
{

    private void OnMouseDown()
    {
        //TODO: Zwischen Block und Detail unterscheiden, damit Abbau und movement ermöglichen
        if (GetComponent<Tree>() != null)
        {
            Destroy(gameObject);
        }
        else if (GetComponent<Rock>() != null)
        {
            Destroy(gameObject);
        }
        else if (GetComponent<Block>() != null)
        {
            Debug.Log("Block Angeklickt: " + gameObject.name);
        }
        //Debug.Log("angeklickt: " + gameObject.name);
    }
}