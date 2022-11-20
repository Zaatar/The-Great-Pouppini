using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharacterController : MonoBehaviour
{
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 12.0f;
    public float StopDistance = 2.5f;
    //helpers
    public Vector3 destination;
    public bool reachedDestination = false;

    private void Update()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if(destinationDistance >= StopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
