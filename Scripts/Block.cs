using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected GameObject plattform;
    //konstruktor
    public Block()
    {
        //TODO: Positions algorythmus erstellen
    }
    public GameObject GetPlattform()
    {
        return plattform;
    }
    public abstract void generate();
}