using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInPlace : MonoBehaviour
{
    public float speed = 1;
    float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0, 360);
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;
        transform.eulerAngles = new Vector3(0, angle, 0);
        if (angle > 360)
            angle = 0;
    }
}
