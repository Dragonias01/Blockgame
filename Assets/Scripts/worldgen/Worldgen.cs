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

    public void GenerateWorld(float bounds, float spacing, int detailspread)
    {
        Block waterplane = block_factory.Generate("water");
        if (waterplane != null)
        {
            waterplane.transform.position = new Vector3(0, -0.25f, 0);
            waterplane.transform.localScale = new Vector3(bounds * 100f, 0.25f, bounds * 100f);
            waterplane.name = "WaterPlane";
            waterplane.generate();
        }
        else
        {
            Debug.LogError("[Worldgen] Wasserfläche konnte nicht erstellt werden.");
        }

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
                block.gameObject.tag = "Detail";

                // Block initialisieren
                block.generate();

                // Detail erstellen
                if (rdetail.Next(0, 100) < detailspread) // 40% Chance für ein Detail
                {
                    Block detail = null;

                    if (rdetail.Next(0, 100) < 50) // 50% Chance für einen Rock statt einem Tree
                    {
                        detail = detail_factory.Generate("rock");
                        if (detail == null)
                        {
                            Debug.LogError($"[Worldgen] Detail konnte nicht erstellt werden bei ({x}, 0.5f, {z}).");
                            continue;
                        }
                    }
                    else
                    {
                        detail = detail_factory.Generate("tree");
                        if (detail == null)
                        {
                            Debug.LogError($"[Worldgen] Detail konnte nicht erstellt werden bei ({x}, 0.25f, {z}).");
                            continue;
                        }
                    }
                    // Position setzen
                    detail.transform.position = new Vector3(x, 0.3f, z) * spacing;
                    detail.name = "detail [" + x + "][" + z + "]";
                    detail.gameObject.tag = "Detail";
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