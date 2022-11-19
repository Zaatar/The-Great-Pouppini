using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool hasBeenPickedUp = false;

    private bool hasBeenDelivered = false;
    [SerializeField] private QuestGiver correspondingQuestGiver;

    public bool getHasBeenPickedUp()
    {
        return hasBeenPickedUp;
    }

    public void setHasBeenPickedUp(bool pHasBeenPickedUp)
    {
        hasBeenPickedUp = pHasBeenPickedUp;
    }
    
    public bool getHasBeenDelivered()
    {
        return hasBeenDelivered;
    }

    public void setHasBeenDelivered(bool pHasBeenDelivered)
    {
        hasBeenDelivered = pHasBeenDelivered;
    }

    public QuestGiver getCorrespondingQuestGiver()
    {
        return correspondingQuestGiver;
    }
    void Update()
    {
        if (hasBeenPickedUp)
        {
            gameObject.SetActive(false);
            Debug.LogWarning("Picked up, deactivating!");
            if (hasBeenDelivered)
            {
                Debug.LogWarning("Pickup delivered to corresponding actor, destroying pickup");
                Destroy(gameObject);
            }
        }
    }
}
