using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour {

    float margin = 10f;
    float panSpeed = 20f;
    public Vector2 mapLimit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetCameraDirection();
	}

    void GetCameraDirection()
    {
        Vector3 pos = transform.position;
        if(Input.mousePosition.y >= Screen.height - margin)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= margin)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - margin)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= margin)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, -mapLimit.x, mapLimit.x);
        pos.z = Mathf.Clamp(pos.z, -mapLimit.y, mapLimit.y);
        transform.position = pos;
    }
}
