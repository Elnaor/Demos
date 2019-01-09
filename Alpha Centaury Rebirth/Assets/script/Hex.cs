using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex {

    public readonly int Q; // Column
    public readonly int R; // Row
    public readonly int S;

    private HexMapManager hexMap;
    private float radius = 1f;


    // Data for map generation and in-game features
    public float Elevation;
    public float Moisture;

    // Q + R+ S = 0
    // S = -(Q + R)
    public Hex(HexMapManager hexMap, int q, int r)
    {
        this.hexMap = hexMap;
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    public Vector3 Position()
    {
        return new Vector3 (HexHorizontalSpacing() * (this.Q + this.R/2f), 0, HexVerticalSpacing() * this.R);
    }

    public float HexHeight()
    {
        return radius * 2;
    }
    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }
    public float HexWidth()
    {
        return HexHeight() * WIDTH_MULTIPLIER;
    }
    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }

    public Vector3 PositionFromCamera (Vector3 CameraPosition, float numRows, float numColumns)
    {

        float mapHeight = numColumns * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();

        Vector3 position = Position();

        if (hexMap.allowWrapEastWest)
        {
            float howManyWidthFromCamera = (position.x - CameraPosition.x) / mapWidth;

            if (Mathf.Abs(howManyWidthFromCamera) <= 0.5f) return position;

            if (howManyWidthFromCamera > 0) howManyWidthFromCamera += 0.5f;
            else howManyWidthFromCamera -= 0.5f;

            int howManyWidthToFix = (int)howManyWidthFromCamera;
            position.x -= howManyWidthToFix * mapWidth;
        }
        if (hexMap.allowWrapNorthSouth)
        {
            float howManyHeightFromCamera = (position.z - CameraPosition.z) / mapHeight;

            if (Mathf.Abs(howManyHeightFromCamera) <= 0.5f) return position;

            if (howManyHeightFromCamera > 0) howManyHeightFromCamera += 0.5f;
            else howManyHeightFromCamera -= 0.5f;

            int howManyHeightToFix = (int)howManyHeightFromCamera;
            position.z -= howManyHeightToFix * mapHeight;
        }
        return position;
        
    }

    public static float Distance (Hex a, Hex b)
    {
        
        int dQ = Mathf.Abs(a.Q - b.Q);
        if (a.hexMap.allowWrapEastWest)
        {
            if (dQ > a.hexMap.numColumns / 2) dQ = a.hexMap.numColumns - dQ;
        }
        int dR = Mathf.Abs(a.R - b.R);
        if (a.hexMap.allowWrapNorthSouth)
        {
            if (dR > a.hexMap.numRows / 2) dR = a.hexMap.numRows - dR;
        }
        return Mathf.Max( 
            dQ, 
            dR, 
            Mathf.Abs(a.S-b.S) 
            );
    }

}
