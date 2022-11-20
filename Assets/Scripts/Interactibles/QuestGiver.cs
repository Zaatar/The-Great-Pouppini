using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest= null;
    public GameObject minimapIcon = null;
    public PlayerState player;

    void Start()
    {
        quest.questGiver = this;
    }
}
