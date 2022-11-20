using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{

    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }
}
