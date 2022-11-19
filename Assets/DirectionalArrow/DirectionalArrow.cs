using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    
    [SerializeField]

    private Transform target;

    private void update()
    {
        transform.LookAt(target);
    }
}