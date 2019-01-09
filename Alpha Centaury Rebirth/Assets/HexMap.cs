using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexMap : MonoBehaviour {

    public GameObject HexPrefab;
    public Camera cam;

    public int test = 0;

    public Mesh MeshWater;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountains;
    public Mesh MeshForest;

    public Material MatOcean;
    public Material MatPlains;
    public Material MatGrassLands;
    public Material MatMountains;

    public float HeightMountain = 1f;
    public float HeightHill = 0.6f;
    public float HeightFlat = 0f;

    public int numRows = 30;
    public int numColumns = 60;

    public bool allowWrapEastWest = true;
    public bool allowWrapNorthSouth = false;

    private Hex[,] hexes;
    public Dictionary<Hex, GameObject> hexToGameObjectMap;

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array not yet instantiated");
            return null;
        }
        if (x < 0) x += numRows;
        if (y < 0) y += numColumns;
        if (allowWrapEastWest)
        {
            x = x % numColumns;
            if (x < 0) x += numColumns;
        }
        if (allowWrapNorthSouth)
        {
            y = y % numRows;
            if (y < 0) y += numRows;
        }

        return hexes[x , y];
    }

    public Hex[] GetHexesWithinRangeOf (Hex centerHex, int radius)
    {
        List<Hex> results = new List<Hex> ();
        for (int dx = -radius; dx < radius-1; dx++)
        {
            for (int dy = Mathf.Max(-radius+1, -dx-radius); dy < Mathf.Min(radius, -dx+radius-1); dy++)
            {
                results.Add(GetHexAt(centerHex.Q+dx, centerHex.R+dy));
            }
        }
        return results.ToArray();
    }

    // Use this for initialization
    void Start() {

        GenerateMap();

    }

    virtual public void GenerateMap()
    {

        hexes = new Hex[numColumns, numRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();
        
        // Generate a fully ocean Map
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = new Hex(this, column, row);
                h.Elevation = -0.5f;
                test++;
                hexes[column, row] = h;
                
                Vector3 pos = h.PositionFromCamera(cam.transform.position, numRows, numColumns);
                GameObject hexGo = (GameObject)Instantiate(HexPrefab, pos, Quaternion.identity, this.transform);
                Debug.Log("1");
                hexToGameObjectMap[h] = hexGo;
                Debug.Log("2");
                hexGo.GetComponent<HexComponent>().hex = h;
                Debug.Log("3");
                hexGo.GetComponent<HexComponent>().hexMap = this;
                Debug.Log("4");

                hexGo.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
                Debug.Log("5");
            }
        }

    }

    public void UpdateHexVisuals()
    {

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = hexes[column, row];
                GameObject hexGo = hexToGameObjectMap[h];

                MeshRenderer mr = hexGo.GetComponentInChildren<MeshRenderer>();
                MeshFilter mf = hexGo.GetComponentInChildren<MeshFilter>();


                if (h.Elevation > HeightMountain)
                {
                    mr.material = MatMountains;
                    mf.mesh = MeshMountains;
                }
                else if (h.Elevation > HeightHill)
                {
                    mr.material = MatGrassLands;
                    mf.mesh = MeshHill;
                }
                else if (h.Elevation > HeightFlat)
                {
                    mr.material = MatPlains;
                    mf.mesh = MeshFlat;

                }
                else
                {
                    mr.material = MatOcean;
                    mf.mesh = MeshWater;
                }
                                      
            }
        }
    }
}
