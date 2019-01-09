using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMapManager {




    public override void GenerateMap()
    {
        // Generate a semi random Map with Continents
        // First, call the base version to make all the hexes
        base.GenerateMap();

        int numContinents = 2;
        int continentSpacing = numColumns / numRows;
        for (int c = 0; c < numContinents; c++)
        {
            int numSplats = Random.Range(4, 8);
            for (int i = 0; i < numSplats; i++)
            {
                int range = Random.Range(5, 8);
                int y = Random.Range(range, numRows - range);
                int x = Random.Range(0, 10) - y / 2 + (c* continentSpacing);

                ElevateArea(x, y, range);

            }
        }

        float noiseResolution = 0.1f;
        Vector2 noiseOffset = new Vector2(Random.Range(0f,1f), Random.Range(0f,1f));
        float noiseScale = 2f; // Larger values mean more larger islands and lakes
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {

                Hex h = GetHexAt(column, row);
                h.Elevation += (Mathf.PerlinNoise( ((float)column / Mathf.Max(numColumns, numRows) / noiseResolution) 
                    + noiseOffset.x, ((float)row / Mathf.Max(numColumns, numRows) / noiseResolution) 
                    + noiseOffset.y )-0.5f) * noiseScale;

            }
        }



                UpdateHexVisuals();
        Debug.Log(test);
    }

    void ElevateArea (int q, int r, int radius, float centerHeight = 0.8f)
    {

        Hex centerHex = GetHexAt(q, r);

        Hex[] areaHexes = GetHexesWithinRangeOf(centerHex, radius);

           foreach (Hex h in areaHexes)
        {
            //if (h.Elevation < 0) h.Elevation = 0;

            h.Elevation = centerHeight * Mathf.Lerp(1f, 0.25f, Mathf.Pow(Hex.Distance(centerHex, h) / radius,2f));

        }
    }



}
