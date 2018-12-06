using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    ControlGroupManager cgm;
    CameraHotkeyManager chm;

    public bool StartedSelection;
    Vector3 initialPoint;
    Vector3 currMousePos;
    Rect rect;
    Color colorrect;

    public Camera cam;


    GameObject[] selections;

    void Start()
    {
        StartedSelection = false;
        cgm = GetComponent<ControlGroupManager>();
        chm = GetComponent<CameraHotkeyManager>();
    }

    void Update()
    {
        currMousePos = Input.mousePosition;
        LeftMouseClick();
        RightMouseClick();
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var unit in FindObjectsOfType<Unit>())
            {
                unit.orderList.Add("holdPosition");
            }
        }
        SelectUnit();
        OnGUI();
    }

    public void UnSelectUnits()
    {
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            if (unit.unitIsSelected)
            {
                unit.unitIsSelected = false;
                unit.gotClicked = false;
                selections = GameObject.FindGameObjectsWithTag("selection");
                for (int i = 0; i < selections.Length; i++)
                {
                    Destroy(selections[i].gameObject);
                }
            }
        }
    }

    public void UnSelectUnitsWithList(List<GameObject> list_)
    {
        for (int j = 0; j < cgm.everyUnit.Count; j++)
        {
            cgm.everyUnit[j].GetComponent<Unit>().unitIsSelected = false;
            cgm.everyUnit[j].GetComponent<Unit>().gotClicked = false;
            selections = GameObject.FindGameObjectsWithTag("selection");
            for (int i = 0; i < selections.Length; i++)
            {
                Destroy(selections[i].gameObject);
            }
        }
    }

    void LeftMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartedSelection = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartedSelection = true;
            initialPoint = Input.mousePosition;

            if (!cgm.isHoldingShift) {
                UnSelectUnits();
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "unit")
                {
                    hit.transform.GetComponent<Unit>().gotClicked = true;
                }
            }
        }
    }

    void RightMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                {
                    foreach (var unit in FindObjectsOfType<Unit>())
                    {
                        if (!cgm.isHoldingShift)
                        {
                            if (unit.unitIsSelected)
                            {
                                if (unit.moveDestinations.Count == 0)
                                {
                                unit.moveDestinations.Add(new Vector3(0, 0, 0));
                                }
                                unit.moveDestinations[0] = hit.point;
                                //unit.Move();
                                unit.orderList.Clear();
                                unit.orderList.Add("move");
                            }
                        }
                        if (cgm.isHoldingShift)
                        { 
                           unit.moveDestinations.Add(hit.point);
                            if (unit.unitIsSelected)
                            {
                                unit.orderList.Add("move");
                            }
                        }
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (StartedSelection == true)
        {
            rect = GetScreenRect(initialPoint, currMousePos);
            colorrect = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            EditorGUI.DrawRect(rect, colorrect);
        }
    }
    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    { //UNDERSTAND THIS
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
        Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public Bounds GetViewportBounds(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // UNDERSTAND THIS:
        //In order to determine which objects are within the bounds of the selection box, we need to bring both the selectable objects and the selection box into the same space.
        //Your fist idea might be to run these tests in world space, but I personally prefer doing it in post-projection (viewport) space because it can be a bit tricky to convert a 
        //selection box into world space if your camera uses a perspective projection. With a perspective projection, the world shape of the selection box is a frustum. You’d have to calculate
        //the correct viewprojection matrix, extract the frustum planes, and then test against all 6 planes.
        //Getting the viewport bounds for a selection box is much easier:
        Vector3 v1 = cam.ScreenToViewportPoint(screenPosition1);
        Vector3 v2 = cam.ScreenToViewportPoint(screenPosition2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = cam.nearClipPlane;
        max.z = cam.farClipPlane;

        Bounds selectionBounds = new Bounds();
        selectionBounds.SetMinMax(min, max);
        return selectionBounds;
    }

    public bool IsWithinSelectionBounds(GameObject go)
    {
        if (StartedSelection == false)
        {
            return false;
        }
        Bounds viewportBounds = GetViewportBounds(initialPoint, currMousePos);
        return viewportBounds.Contains(cam.WorldToViewportPoint(go.transform.position));
    }

    public void SelectUnit()
    {
        if (StartedSelection||cgm.controlGroupPressed)
        {
            foreach (var unit in FindObjectsOfType<Unit>())
            {
                if (IsWithinSelectionBounds(unit.gameObject) || unit.gotClicked)
                {
                    if (unit.unitIsSelected == false)
                    {
                        GameObject unitselection = Instantiate(unit.GetComponent<Unit>().selection, unit.gameObject.transform.position, Quaternion.identity, unit.gameObject.transform);
                        unitselection.transform.SetParent(unit.gameObject.transform, false);
                        unit.unitIsSelected = true;
                    }
                }
            }
        }
    }
}


