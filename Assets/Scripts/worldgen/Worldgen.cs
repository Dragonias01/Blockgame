using UnityEngine;

public class Worldgen
{
    private Factory block_factory = new Block_factory();
    private Factory detail_factory = new Detail_factory();
    private System.Random rdetail = new System.Random();

    public Worldgen() { }

    public void GenerateWorld(float bounds, float spacing, int detailspread)
    {
        // --- Wasserflaeche ---
        Block waterplane = block_factory.Generate("water") as Block;
        if (waterplane != null)
        {
            waterplane.transform.position = new Vector3(0, -0.25f, 0);
            waterplane.transform.localScale = new Vector3(bounds * 100f, 0.25f, bounds * 100f);
            waterplane.name = "WaterPlane";
            waterplane.generate();
        }
        else
        {
            Debug.LogError("[Worldgen] Wasserflaeche konnte nicht erstellt werden.");
        }

        // --- Welt-Tiles ---
        for (float x = -bounds; x <= bounds; x++)
        {
            for (float z = -bounds; z <= bounds; z++)
            {
                // Block erstellen und auf Block casten
                Block block = block_factory.Generate("default1") as Block;

                if (block == null)
                {
                    Debug.LogError($"[Worldgen] Block konnte nicht erstellt werden bei ({x}, 0, {z}).");
                    continue;
                }

                block.transform.position = new Vector3(x, 0, z) * spacing;
                block.name = "tile [" + x + "][" + z + "]";
                block.gameObject.tag = "Detail";
                block.generate();

                // Detail mit konfigurierter Wahrscheinlichkeit spawnen
                if (rdetail.Next(0, 100) < detailspread)
                {
                    string detailType = rdetail.Next(0, 100) < 50 ? "rock" : "tree";
                    Block detail = detail_factory.Generate(detailType) as Block;

                    if (detail == null)
                    {
                        Debug.LogError($"[Worldgen] Detail '{detailType}' konnte nicht erstellt werden bei ({x}, 0.3f, {z}).");
                        continue;
                    }

                    detail.transform.position = new Vector3(x, 0.3f, z) * spacing;
                    detail.name = "detail [" + x + "][" + z + "]";
                    detail.gameObject.tag = "Detail";
                    detail.generate();
                }
            }
        }
    }
}