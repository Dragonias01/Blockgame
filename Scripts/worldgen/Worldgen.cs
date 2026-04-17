using UnityEngine;

public class Worldgen
{

    // private int bounds = 5;
    private Factory block_factory = new Block_factory();
    private Factory detail_factory = new Detail_factory();
    //Details zufällig generieren
    private System.Random rdetail = new System.Random();

    public Worldgen()
    {
        // Leer lassen

    }

    public void GenerateWorld(float bounds, float spacing)
    {
        for (float x = -bounds; x <= bounds; x++)
        {
            for (float z = -bounds; z <= bounds; z++)
            {
                // Block erstellen
                Block block = block_factory.Generate("default1");

                if (block == null)
                {
                    Debug.LogError($"[Worldgen] Block konnte nicht erstellt werden bei ({x}, 0, {z}).");
                    continue;
                }

                // Position setzen
                block.transform.position = new Vector3(x, 0, z) * spacing;
                block.name = "tile [" + x + "][" + z + "]";
                // Block initialisieren
                block.generate();

                // Detail erstellen
                if (rdetail.Next(0, 5) < 2) // 40% Chance für ein Detail
                {
                    Block detail = detail_factory.Generate("detail1");
                    if (detail == null)
                    {
                        Debug.LogError($"[Worldgen] Detail konnte nicht erstellt werden bei ({x}, 0, {z}).");
                        continue;
                    }

                    // Position setzen
                    detail.transform.position = new Vector3(x, 0.5f, z) * spacing;
                    detail.name = "detail [" + x + "][" + z + "]";

                    // Detail initialisieren
                    detail.generate();
                }
                else
                {
                    continue; // Kein Detail generieren, weiter zum nächsten Block
                }

            }
        }
    }
}