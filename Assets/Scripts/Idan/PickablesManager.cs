using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject pickablePrefab;
    public Transform[] pickableSpawnLocations;
    private List<Transform> usedSpawnLocations = new List<Transform>();
    private HumanCharacterController[] humansList;

    private void Start()
    {
        Invoke("GetHumansReference", 5.0f);
    }
    public void GetHumansReference()
    {
        humansList = FindObjectsOfType<HumanCharacterController>();
        for (int i = 0; i < 3; i++)
        {
            SpawnQuest();
        }
    }

    public void SpawnQuest()
    {
        Transform spawnLocation;
        do
        {
            spawnLocation = pickableSpawnLocations[Random.Range(0, pickableSpawnLocations.Length)];
        } while (usedSpawnLocations.Contains(spawnLocation));

        usedSpawnLocations.Add(spawnLocation);
        GameObject obj = Instantiate(pickablePrefab,spawnLocation);
        PickupObject pickup = obj.GetComponent<PickupObject>();

        pickup.spawnPoint = spawnLocation;

        HumanCharacterController humanQuestGiver;
        do
        {
            humanQuestGiver = humansList[Random.Range(0, humansList.Length)];
        } while (humanQuestGiver.GetComponent<QuestGiver>() != null);

        humanQuestGiver.gameObject.AddComponent<QuestGiver>();

        humanQuestGiver.GetComponent<QuestGiver>().quest = new Quest();
        humanQuestGiver.GetComponent<QuestGiver>().quest.pickupObject = pickup;
        pickup.correspondingQuestGiver = humanQuestGiver.GetComponent<QuestGiver>();
        humanQuestGiver.Stop();
    }

    public void DestroyPickable(PickupObject pickable)
    {
        pickable.setHasBeenDelivered(true);
        QuestGiver questGiver = pickable.getCorrespondingQuestGiver();
        HumanCharacterController controller = questGiver.GetComponent<HumanCharacterController>();
        
        controller.KeepGoing();

        Destroy(pickable.gameObject, 2.0f);
        Destroy(questGiver, 2.0f);
        usedSpawnLocations.Remove(pickable.spawnPoint);
    }
}
