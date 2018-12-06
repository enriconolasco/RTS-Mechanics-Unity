using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHotkeyManager : MonoBehaviour {
    ControlGroupManager cgm;
    public Camera mainCam;

    public List<Vector3> cameraLocations;
    bool canAddCamLocations;
	
    void Start()
    {
        canAddCamLocations = true;
        cgm = GetComponent<ControlGroupManager>();
    }

    void Update()
    {
        Manager();
    }

    public void Manager()
    {
        if(cameraLocations.Count > 8)
        {
            canAddCamLocations = false;
        }
        CreateCameraLocation();
        GoToCameraLocation();
    }

    public void AddCamLocationSlot()
    {
        if(canAddCamLocations == true)
        cameraLocations.Add(new Vector3(0, 0, 0));
    }

    public void CreateCameraLocation()
    {
        if (Input.GetKeyDown(KeyCode.F1) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[0] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F2) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[1] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F3) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[2] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F4) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[3] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F5) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[4] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F6) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[5] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F7) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[6] = mainCam.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F8) && cgm.isHoldingCtrl)
        {
            AddCamLocationSlot();
            cameraLocations[7] = mainCam.transform.position;
        }
    }

    public void GoToCameraLocation()
    {
        if(Input.GetKeyDown(KeyCode.F1) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[0];
        }
        if (Input.GetKeyDown(KeyCode.F2) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[1];
        }
        if (Input.GetKeyDown(KeyCode.F3) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[2];
        }
        if (Input.GetKeyDown(KeyCode.F4) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[3];
        }
        if (Input.GetKeyDown(KeyCode.F5) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[4];
        }
        if (Input.GetKeyDown(KeyCode.F6) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[5];
        }
        if (Input.GetKeyDown(KeyCode.F7) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[6];
        }
        if (Input.GetKeyDown(KeyCode.F8) && !cgm.isHoldingCtrl)
        {
            mainCam.transform.position = cameraLocations[7];
        }
    }
}
