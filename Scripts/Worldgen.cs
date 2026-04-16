using UnityEngine;

public class Worldgen
{
    // private int bounds = 5;
    private Factory block_factory = new Block_factory();

    public Worldgen()
    {
        // Leer lassen
    }

    public void GenerateWorld(int bounds, int spacing)
    {
        for (int x = -bounds; x <= bounds; x++)
        {
            for (int z = -bounds; z <= bounds; z++)
            {
                // Block erstellen
                Block block = block_factory.Generate("default1");

                if (block == null)
                {
                    Debug.LogError($"[Worldgen] Block konnte nicht erstellt werden bei ({x}, 0, {z}).");
                    continue;
                }

                // Position setzen
                block.transform.position = new Vector3(x + spacing, 0, z + spacing);

                // Block initialisieren
                block.generate();
            }
        }
    }
}