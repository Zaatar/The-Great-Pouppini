using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    [SerializeField]

    private GameObject player;
    public GameObject arrow;
    public float speed;
    public float maxAmp;
    public float minAmp;


    private void Update()
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        List<Quest> quests = playerState.quests;

        if (playerState.quests.Count > 0)
        {
            Quest currentQuest = quests[quests.Count - 1];

            if (quests[quests.Count - 1].pickupObject != null)
            {
                arrow.GetComponent<MeshRenderer>().enabled = true;
                
                if(currentQuest.pickupObject.getCanBePickedUp())
                {
                    transform.LookAt(currentQuest.pickupObject.transform);

                }
                if(currentQuest.pickupObject.getHasBeenPickedUp())
                {
                    Debug.LogWarning("point to quest giver");
                    transform.LookAt(currentQuest.questGiver.transform);
                }
                //if(currentQuest.questComplete)
                //{
                //    currentQuest = quests[quests.Count - 1];


                //}
            }
        }
        else
        {
            arrow.GetComponent<MeshRenderer>().enabled = false;
        }

        Vector3 vec = new Vector3(maxAmp + minAmp * Mathf.Sin(Time.time*speed), maxAmp + minAmp * Mathf.Sin(Time.time*speed) , maxAmp + minAmp * Mathf.Sin(Time.time*speed));

        arrow.transform.localScale = vec;

    }
}