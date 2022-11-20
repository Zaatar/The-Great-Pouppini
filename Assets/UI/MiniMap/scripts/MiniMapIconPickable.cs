using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconPickable : MonoBehaviour
{

    public GameObject target;

    void Start()
    {
        this.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<PickupObject>().getCanBePickedUp())
        {
            this.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = true;
        }
        else if (target.GetComponent<PickupObject>().getHasBeenPickedUp())
        {
            this.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
        }

        transform.position = target.transform.position;
    }
}
