using System.Numerics;
using UnityEngine;

public abstract class Block : MonoBehaviour
{

    public void block()
    {
        plattform = gameObject;
    }
    protected GameObject plattform;

    public GameObject GetPlattform()
    {
        return plattform;
    }

    public abstract void generate();
}