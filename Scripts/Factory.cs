using UnityEngine;


public abstract class Factory
{
    //konstruktor
    public Factory(GameObject defaultPrefab) { }
    public abstract GameObject Generate(string type);
}
