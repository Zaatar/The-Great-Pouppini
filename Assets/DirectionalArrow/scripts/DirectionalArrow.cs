using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    
    [SerializeField]

    private Transform target;
    public Transform arrow;
    public float speed;
    public float maxAmp;
    public float minAmp;
    public Transform camera;
    private void Update()
    {

        transform.LookAt(target);
        //transform.position = new Vector3(transform.position.x, -camera.position.y, transform.position.z);

        Vector3 vec = new Vector3(maxAmp + minAmp * Mathf.Sin(Time.time*speed), maxAmp + minAmp * Mathf.Sin(Time.time*speed) , maxAmp + minAmp * Mathf.Sin(Time.time*speed));

        arrow.localScale = vec;

    }
}