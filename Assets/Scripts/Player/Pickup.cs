using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int numberOfSlots = 3;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PickablesManager pickablesManager;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject HeartPrefab;
    [SerializeField] private GameObject BrokenHeartPrefab;
    private bool currentlyInPickupObjectRange = false;
    private bool currentlyInDeliverObjectRange = false;
    private PickupObject objectToBePickedUp = null;
    private QuestGiver questGiverInProximity = null;

    void Update()
    {
        if (currentlyInPickupObjectRange && Input.GetKeyDown(KeyCode.E))
        {
            if (objectToBePickedUp != null && numberOfSlots != 0)
            {
                if (playerState != null && playerState.getQuests().Count != 0)
                {

                    collectPickup();
                }
            }
        }
        if (currentlyInDeliverObjectRange && Input.GetKeyDown(KeyCode.E))
        {
            if (questGiverInProximity != null)
            {
                if (questGiverInProximity.quest != null && questGiverInProximity.quest.pickupObject != null)
                {
                    //Get Quest from Quest Giver
                    if (!questGiverInProximity.quest.isActive)
                    {
                        if (playerState.getQuestsLimit() > 0)
                        {
                            GameObject heart = Instantiate(HeartPrefab);
                            GameObject brokenHeart = Instantiate(BrokenHeartPrefab);

                            heart.transform.position = transform.position + Vector3.up;
                            brokenHeart.transform.position = questGiverInProximity.transform.position + Vector3.up * 2;

                            Vector3 destinationDirection = transform.position - questGiverInProximity.transform.position;
                            destinationDirection.y = 0;

                            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                            questGiverInProximity.transform.rotation = Quaternion.RotateTowards(
                                questGiverInProximity.transform.rotation, targetRotation, 360);

                            playerState.addQuest(questGiverInProximity.quest);
                            questGiverInProximity.quest.isActive = true;
                            playerState.setQuestsLimit(playerState.getQuestsLimit()-1);
                            questGiverInProximity.quest.pickupObject.setCanBePickedUp(true);
                            FindObjectOfType<AudioManager>().Play("QuestAccepted");
                        }
                    }
                    else
                    {
                        //Deliver Item to Quest Giver
                        deliverItemToQuestGiver();
                    }
                }
            }
        }
    }

    private void deliverItemToQuestGiver()
    {
        if (numberOfSlots != 3)
        {
            foreach (PickupObject heldObject in playerState.getPickups())
            {
                if (heldObject.getCorrespondingQuestGiver() == questGiverInProximity &&
                    !heldObject.getHasBeenDelivered())
                {
                    pickablesManager.DestroyPickable(heldObject);

                    Vector3 destinationDirection = transform.position - questGiverInProximity.transform.position;
                    destinationDirection.y = 0;

                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    questGiverInProximity.transform.rotation = Quaternion.RotateTowards(
                        questGiverInProximity.transform.rotation, targetRotation, 360);

                    GameObject heart = Instantiate(HeartPrefab);
                    GameObject heartQuestGiver = Instantiate(HeartPrefab);

                    heart.transform.position = transform.position + Vector3.up;
                    heartQuestGiver.transform.position = questGiverInProximity.transform.position + Vector3.up;

                    questGiverInProximity.quest.questComplete = true;
                    playerState.updateScore(questGiverInProximity.quest.pointsReward);
                    timer.timeRemaining += questGiverInProximity.quest.questCompletionTimeReward;
                    numberOfSlots++;
                    playerState.setQuestsLimit(playerState.getQuestsLimit() + 1);
                    playerState.quests.RemoveAt(playerState.quests.Count - 1);
                    Debug.LogWarning("Delivered Object");
                    FindObjectOfType<AudioManager>().Play("QuestSuccess");
                }
            }
        }
    }

    private void collectPickup()
    {
        if (objectToBePickedUp.getCanBePickedUp() && !objectToBePickedUp.getHasBeenPickedUp())
        {
            objectToBePickedUp.setHasBeenPickedUp(true);
            playerState.addPickup(objectToBePickedUp);
            numberOfSlots--;
            Debug.LogWarning("Picked Up Object");
            FindObjectOfType<AudioManager>().Play("PickupItem");
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
