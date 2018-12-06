using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGroupManager : MonoBehaviour {
    public List<GameObject> controlGroup1;
    public List<GameObject> controlGroup2;
    public List<GameObject> controlGroup3;
    public List<GameObject> controlGroup4;
    public List<GameObject> controlGroup5;

    public List<GameObject> everyUnit;

    PlayerController pc;
    RTSCameraController rtscc;

    public bool isHoldingCtrl;
    public bool isHoldingShift;

    public bool controlGroupPressed;

    float timer = 0;
    bool startTimer;
    public Camera mainCam;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        rtscc = GetComponent<RTSCameraController>();
        controlGroupPressed = false;
        isHoldingCtrl = false;
        isHoldingShift = false;
        startTimer = false;
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            everyUnit.Add(unit.gameObject);
        }
    }
    
    void Update()
    {
        Timer();
        CheckIfHoldingCtrl();
        CheckIfHoldingShift();
        AddToControlGroup();
        GoToControlGroup();
    }

    public void CheckIfHoldingCtrl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isHoldingCtrl = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isHoldingCtrl = false;
        }
        
    }

    public void CheckIfHoldingShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isHoldingShift = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isHoldingShift = false;
        }
    }

    public void AddToControlGroup()
    {
        if (isHoldingCtrl)
        {
            foreach (var unit in FindObjectsOfType<Unit>())
            {
                if (unit.unitIsSelected && unit.addedToControlGroup1 == false && Input.GetKeyDown(KeyCode.Q))
                {
                    controlGroup1.Add(unit.gameObject);
                    unit.addedToControlGroup1 = true;
                }
                if (unit.unitIsSelected && unit.addedToControlGroup2 == false && Input.GetKeyDown(KeyCode.W))
                {
                    controlGroup2.Add(unit.gameObject);
                    unit.addedToControlGroup2 = true;
                }
                if (unit.unitIsSelected && unit.addedToControlGroup3 == false && Input.GetKeyDown(KeyCode.E))
                {
                    controlGroup3.Add(unit.gameObject);
                    unit.addedToControlGroup3 = true;
                }
                if (unit.unitIsSelected && unit.addedToControlGroup5 == false && Input.GetKeyDown(KeyCode.T))
                {
                    controlGroup5.Add(unit.gameObject);
                    unit.addedToControlGroup5 = true;
                }
                if (unit.unitIsSelected && unit.addedToControlGroup4 == false && Input.GetKeyDown(KeyCode.G))
                {
                    controlGroup4.Add(unit.gameObject);
                    unit.addedToControlGroup4 = true;
                }
            }
        }
    }

    void Timer()
    {
        if (startTimer == true)
        {
            timer += Time.deltaTime;
        }

        if (startTimer == false)
            timer = 0;
    }

    public void GoToControlGroup()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for (int i = 0; i < everyUnit.Count; i++)
            {
                everyUnit[i].GetComponent<Unit>().gotClicked = true;
            }
        }
        if (isHoldingCtrl == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (timer == 0 || timer >= 0.2)
                {
                    timer = 0;
                    startTimer = true;
                    pc.UnSelectUnitsWithList(everyUnit);
                    for (int i = 0; i < controlGroup1.Count; i++)
                    {
                        controlGroup1[i].GetComponent<Unit>().gotClicked = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Q) && timer != 0)
                {
                    startTimer = false;
                    mainCam.transform.position = new Vector3(controlGroup1[0].transform.position.x, mainCam.transform.position.y, controlGroup1[0].transform.position.z / Screen.height);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (timer == 0 || timer >= 0.2)
                {
                    timer = 0;
                    startTimer = true;
                    pc.UnSelectUnitsWithList(everyUnit);
                    for (int i = 0; i < controlGroup2.Count; i++)
                    {
                        controlGroup2[i].GetComponent<Unit>().gotClicked = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W) && timer != 0)
                {
                    startTimer = false;
                    mainCam.transform.position = new Vector3(controlGroup2[0].transform.position.x, mainCam.transform.position.y, controlGroup2[0].transform.position.z / Screen.height);
                }
            }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (timer == 0 || timer >= 0.2)
                    {
                        timer = 0;
                        startTimer = true;
                        pc.UnSelectUnitsWithList(everyUnit);
                        for (int i = 0; i < controlGroup3.Count; i++)
                        {
                            controlGroup3[i].GetComponent<Unit>().gotClicked = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && timer != 0)
                    {
                        startTimer = false;
                        mainCam.transform.position = new Vector3(controlGroup3[0].transform.position.x, mainCam.transform.position.y, controlGroup3[0].transform.position.z / Screen.height);
                    }
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (timer == 0 || timer >= 0.2)
                    {
                        timer = 0;
                        startTimer = true;
                        pc.UnSelectUnitsWithList(everyUnit);
                        for (int i = 0; i < controlGroup4.Count; i++)
                        {
                            controlGroup4[i].GetComponent<Unit>().gotClicked = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.G) && timer != 0)
                    {
                        startTimer = false;
                    mainCam.transform.position = new Vector3(controlGroup4[0].transform.position.x, mainCam.transform.position.y, controlGroup4[0].transform.position.z/Screen.height);
                    }
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (timer == 0 || timer >= 0.2)
                    {
                        timer = 0;
                        startTimer = true;
                        pc.UnSelectUnitsWithList(everyUnit);
                        for (int i = 0; i < controlGroup5.Count; i++)
                        {
                            controlGroup5[i].GetComponent<Unit>().gotClicked = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.T) && timer != 0)
                    {
                        startTimer = false;
                        mainCam.transform.position = new Vector3(controlGroup5[0].transform.position.x, mainCam.transform.position.y, controlGroup5[0].transform.position.z / Screen.height);
                    }
                }
            
        }
        controlGroupPressed = true;
        pc.SelectUnit();
        controlGroupPressed = false;
    }
        
    
}
