using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

    Vector3 oldPosition;
    HexComponent[] hexes;

    // Use this for initialization
    void Start () {
        oldPosition = this.transform.position;
	}
	
     
	// Update is called once per frame
	void Update () {
        CheckIfCameraMoved();
	}

    public void PanToHex (Hex hex)
    {
        
    }

    void CheckIfCameraMoved()
    {
        if(oldPosition != this.transform.position)
        {

            oldPosition = this.transform.position;

            hexes = GameObject.FindObjectsOfType<HexComponent>();

            foreach(HexComponent hex in hexes)
            {
                hex.UpdatePosition();
            }
        }
    }

}
