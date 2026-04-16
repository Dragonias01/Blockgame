using UnityEngine;

public class Worldgen
{
    //einstellungs feld für Unity Einstellungen
    [SerializeField] private int bounds = 5;
    Factory block_factory = new Block_factory();
    public Worldgen()
    {

        Block default1 = block_factory.Generate("default1");

        //mit generate Ausführen
        default1.generate();

        //TODO: Auf Unity Logik um modelieren
        //spielfeld.Children.Add(default1.GetPlattform());

    }

}
