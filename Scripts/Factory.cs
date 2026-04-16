using UnityEngine;


abstract class Factory
{
    //konstruktor
    public Factory() { }
    public abstract GameObject generate(string type);
}
