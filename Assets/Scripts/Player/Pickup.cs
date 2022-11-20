using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int numberOfSlots = 3;
    private bool currentlyInPickupObjectRange = false;
    private bool currentlyInDeliverObjectRange = false;
    private PickupObject objectToBePickedUp = null;
    private QuestGiver questGiverInProximity = null;
    private List<PickupObject> pickups = new List<PickupObject>();

    void Update()
    {
        if (currentlyInPickupObjectRange && Input.GetKeyDown(KeyCode.E))
        {
            if (objectToBePickedUp != null && numberOfSlots != 0 && !objectToBePickedUp.getHasBeenPickedUp())
            {
                objectToBePickedUp.setHasBeenPickedUp(true);
                pickups.Add(objectToBePickedUp);
                numberOfSlots--;
                Debug.LogWarning("Picked Up Object");
            }
        }
        if (currentlyInDeliverObjectRange && Input.GetKeyDown(KeyCode.E))
        {
            if (questGiverInProximity != null && numberOfSlots != 3)
            {
                foreach (PickupObject heldObject in pickups)
                {
                    if (heldObject.getCorrespondingQuestGiver() == questGiverInProximity && 
                        !heldObject.getHasBeenDelivered())
                    {
                        heldObject.setHasBeenDelivered(true);
                        Debug.LogWarning("Delivered Object");
                        numberOfSlots++;
                    }
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.transform.gameObject.GetComponent<PickupObject>() != null)
        {
            currentlyInPickupObjectRange = true;
            objectToBePickedUp = objectCollidedWith.transform.gameObject.GetComponent<PickupObject>();
        }

        if (objectCollidedWith.transform.gameObject.GetComponent<QuestGiver>() != null)
        {
            currentlyInDeliverObjectRange = true;
            questGiverInProximity = objectCollidedWith.transform.gameObject.GetComponent<QuestGiver>();
        }
    }

    void OnTriggerExit(Collider objectCollidedWith)
    {
        if (objectCollidedWith.transform.gameObject.GetComponent<PickupObject>() != null)
        {
            currentlyInPickupObjectRange = false;
            objectToBePickedUp = null;
        }
		
        if (objectCollidedWith.transform.gameObject.GetComponent<QuestGiver>() != null)
        {
            currentlyInDeliverObjectRange = false;
            questGiverInProximity = null;
        }
    }
}
