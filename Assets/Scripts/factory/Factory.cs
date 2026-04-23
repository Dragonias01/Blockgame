using UnityEngine;


public abstract class Factory
{
    public Factory() { }

    public abstract Object Generate(string type);
}