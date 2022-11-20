using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public string description;
    public int pointsReward = 25;
    private float timeReward;
    public bool questComplete;
    public PickupObject pickupObject;
    public QuestGiver questGiver;
    public float questCompletionTimeReward = 10.0f;
}
