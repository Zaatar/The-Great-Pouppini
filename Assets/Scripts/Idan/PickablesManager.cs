using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject pickablePrefab;
    public GameObject humanMinimapIcon;
    public Transform[] pickableSpawnLocations;
    private List<Transform> usedSpawnLocations = new List<Transform>();
    private HumanNavigator[] humansList;

    private void Start()
    {
        Invoke("GetHumansReference", 5.0f);
    }
    public void GetHumansReference()
    {
        humansList = FindObjectsOfType<HumanNavigator>();
        for (int i = 0; i < 6; i++)
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

        HumanNavigator humanQuestGiver;
        do
        {
            humanQuestGiver = humansList[Random.Range(0, humansList.Length)];
        } while (humanQuestGiver.GetComponent<QuestGiver>() != null);

        humanQuestGiver.gameObject.AddComponent<QuestGiver>();
        GameObject obj2 = Instantiate(humanMinimapIcon, humanQuestGiver.transform);
        obj2.transform.position = obj2.transform.position - Vector3.up * 3;
        humanQuestGiver.GetComponent<QuestGiver>().quest = new Quest();
        humanQuestGiver.GetComponent<QuestGiver>().quest.pickupObject = pickup;
        humanQuestGiver.GetComponent<QuestGiver>().minimapIcon = obj2;

        pickup.correspondingQuestGiver = humanQuestGiver.GetComponent<QuestGiver>();
        humanQuestGiver.GetComponent<HumanCharacterController>().Stop();
    }

    public void DestroyPickable(PickupObject pickable)
    {
        pickable.setHasBeenDelivered(true);
        QuestGiver questGiver = pickable.getCorrespondingQuestGiver();
        HumanCharacterController controller = questGiver.GetComponent<HumanCharacterController>();
        
        controller.KeepGoing();
        Destroy(questGiver.minimapIcon);
        Destroy(pickable.gameObject, 2.0f);
        Destroy(questGiver, 2.0f);
        usedSpawnLocations.Remove(pickable.spawnPoint);

        SpawnQuest();
    }
}
