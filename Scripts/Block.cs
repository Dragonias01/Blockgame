using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected GameObject plattform;

    public GameObject GetPlattform()
    {
        return plattform;
    }
    public abstract void generate();
}