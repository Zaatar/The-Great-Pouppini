using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimationHelper : MonoBehaviour
{
    public ThirdPersonMovement dogController;

    void StopBarking()
    {
        dogController.StopBarking();
    }
}
