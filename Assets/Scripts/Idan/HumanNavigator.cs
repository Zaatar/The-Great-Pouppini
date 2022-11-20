using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanNavigator : MonoBehaviour
{
    HumanCharacterController controller;
    public Waypoint currentWaypoint;
    public bool IsReversed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<HumanCharacterController>();
        controller.SetDestination(currentWaypoint.GetRandomPosition());
        bool isRunning = (Random.Range(0, 10) % 3 == 0);
        GetComponent<Animator>().SetBool((isRunning) ? "IsRunning" : "IsWalking", true);
        controller.MovementSpeed *= ((isRunning) ? 1.5f : 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.reachedDestination)
        {
            currentWaypoint = ((IsReversed)? currentWaypoint.m_previousWaypoint:currentWaypoint.m_nextWaypoint);
            controller.SetDestination(currentWaypoint.GetRandomPosition());
        }
    }
}
