using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionTrigger : MonoBehaviour
{
    public string distractionMask;
    [SerializeField] private ThirdPersonMovement movementScript;
    [SerializeField] private int distractionMultiplier = 2;
    private float originalSpeed;
    private float originalJumpHeight;

    void Start()
    {
        originalSpeed = movementScript.speed;
        originalJumpHeight = movementScript.jumpHeight;
    }
    
    void OnTriggerEnter(Collider objectCollidedWith)
    {
        handleTrigger(objectCollidedWith, true);
    }

    void OnTriggerExit(Collider objectCollidedWith)
    {
        handleTrigger(objectCollidedWith, false);
    }

    void handleTrigger(Collider objectCollidedWith, bool isEntering)
    {
        if (objectCollidedWith.CompareTag(distractionMask))
        {

            if (isEntering)
            {
                movementScript.speed /= distractionMultiplier;
                movementScript.jumpHeight /= distractionMultiplier;
            }
            else
            {
                movementScript.speed = originalSpeed;
                movementScript.jumpHeight = originalJumpHeight;
            }
        }
    }
}
