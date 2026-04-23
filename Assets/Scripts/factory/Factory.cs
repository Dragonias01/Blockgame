using UnityEngine;


public abstract class Factory
{
    // Parameterloser Konstruktor – kein defaultPrefab nötig,
    // da MonoBehaviours über AddComponent erstellt werden.
    public Factory() { }

    //Erstellt einen Block anhand des Typ-Strings und gibt ihn zurück.
    public abstract Object Generate(string type);
}