using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNavigator : MonoBehaviour
{
    HumanCharacterController controller;
    public Waypoint currentWaypoint;
    public bool IsReversed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<HumanCharacterController>();
        controller.SetDestination(currentWaypoint.GetRandomPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.reachedDestination)
        {
            currentWaypoint = currentWaypoint.m_nextWaypoint;
            controller.SetDestination(currentWaypoint.GetRandomPosition());
        }
    }
}
