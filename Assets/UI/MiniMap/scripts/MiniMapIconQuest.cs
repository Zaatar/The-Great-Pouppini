using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconQuest : MonoBehaviour
{

    public GameObject target;

    void Start()
    {
        this.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<QuestGiver>().quest.questComplete)
        {
            this.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
        }

        transform.position = target.transform.position;
    }
}
