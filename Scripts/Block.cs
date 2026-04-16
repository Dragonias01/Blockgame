using UnityEngine;

abstract class Block
{
    protected double posx;
    protected double posy;
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
