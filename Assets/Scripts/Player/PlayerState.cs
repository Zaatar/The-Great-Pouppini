using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();
    private List<PickupObject> pickups = new List<PickupObject>();
    private int points;
    [SerializeField] private int questsLimit = 99;

    public List<Quest> getQuests()
    {
        return quests;
    }

    public void addQuest(Quest pQuest)
    {
        quests.Add(pQuest);
    }

    public List<PickupObject> getPickups()
    {
        return pickups;
    }

    public void addPickup(PickupObject pPickupObject)
    {
        pickups.Add(pPickupObject);
    }

    public int getPoints()
    {
        return points;
    }

    public void updateScore(int pScoreUpdate)
    {
        points += pScoreUpdate;
    }

    public int getQuestsLimit()
    {
        return questsLimit;
    }

    public void setQuestsLimit(int pQuestsLimit)
    {
        questsLimit = pQuestsLimit;
    }
}
