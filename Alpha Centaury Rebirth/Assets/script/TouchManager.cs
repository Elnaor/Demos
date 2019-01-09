using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public Camera cam;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);
            Vector3 mousePosF = cam.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = cam.ScreenToWorldPoint(mousePosNear);
            Debug.DrawRay(mousePosN, mousePosF-mousePosN, Color.green);

            /*RaycastHit hit;
            if (Physics.Raycast(mousePosN, mousePosF-mousePosN, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
            */
        }

    }
}
