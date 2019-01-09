using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour {

    public Hex hex;
    public HexMapManager hexMap;
    //public Camera cam;

    public void UpdatePosition()
    {
        this.transform.position = hex.PositionFromCamera(hexMap.cam.transform.position, hexMap.numRows, hexMap.numColumns);
    }

}
