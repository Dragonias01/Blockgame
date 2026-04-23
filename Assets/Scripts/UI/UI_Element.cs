using UnityEngine;
using UnityEngine.UI;
public abstract class UI_Element : MonoBehaviour
{
    protected GameObject element;

    public GameObject GetElement()
    {
        return element;
    }

    public abstract void CreateElement();
}