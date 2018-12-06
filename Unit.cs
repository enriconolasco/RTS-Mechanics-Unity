using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public GameObject selection;
    public bool unitIsSelected;
    public bool gotClicked;
    public bool addedToControlGroup1;
    public bool addedToControlGroup2;
    public bool addedToControlGroup3;
    public bool addedToControlGroup4;
    public bool addedToControlGroup5;

    public GameObject playerController;
    public ControlGroupManager cgm;
    public SupplyManager sm;

    public bool unitArrivedAtFinalLocation;
    float timeThatIsCollidingWithOtherUnit = 0.0f;
    bool runTimer;
    public List<Vector3> moveDestinations;

    public GameObject unitFinalDestination;
    bool canInstantiateFD = true;
    GameObject[] fds;

    public List<string> orderList;
    string firstOrder;

    void Start()
    {
        cgm = GetComponent<ControlGroupManager>();
        sm = GetComponent<SupplyManager>();

        runTimer = false;

        sm.currSupply += 4;

        unitIsSelected = false;
        gotClicked = false;
        addedToControlGroup1 = false;
        addedToControlGroup2 = false;
        addedToControlGroup3 = false;
        addedToControlGroup4 = false;
        addedToControlGroup5 = false;
    }

    void Update()
    {
        CheckIfUnitArrivedAtFinalMoveLocation();
        OrderListManager();
    }


    void OrderListManager()
    {
        if (orderList[0] == "move")
        {
            if (unitArrivedAtFinalLocation)
            {
                orderList.RemoveAt(0);
                fds = GameObject.FindGameObjectsWithTag("fd");
                Destroy(fds[0]);
            }
            Move();
        }
        if (orderList[0] == "holdPosition")
        {
            HoldPosition();
        }
    }


    public void CheckIfUnitArrivedAtFinalMoveLocation()
    {
        if (gameObject.transform.position.x == moveDestinations[0].x && gameObject.transform.position.z == moveDestinations[0].z)
        {
            moveDestinations.RemoveAt(0);
            unitArrivedAtFinalLocation = true;
            canInstantiateFD = true;
        }
        else
        {
            unitArrivedAtFinalLocation = false;
        }
    }

    public void Move()
    {
        navAgent.SetDestination(moveDestinations[0]);
    }

    public void HoldPosition()
    {
        moveDestinations[0] = gameObject.transform.position;
        Move();
        moveDestinations.Clear();
        orderList.Clear();
    }

    void OnCollisionStay(Collider col)
    {
        if (col.gameObject.GetComponent<Unit>())
        {
            timeThatIsCollidingWithOtherUnit += Time.deltaTime;
        }
    }
}
