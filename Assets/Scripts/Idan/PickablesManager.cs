using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesManager : MonoBehaviour
{
    public GameObject pickablePrefab;
    public Transform[] pickableSpawnLocations;
    private List<Transform> usedSpawnLocations = new List<Transform>();
    public GameObject SpawnPickable()
    {
        Transform spawnLocation;
        do
        {
            spawnLocation = pickableSpawnLocations[Random.Range(0, pickableSpawnLocations.Length)];
        } while (usedSpawnLocations.Contains(spawnLocation));

        usedSpawnLocations.Add(spawnLocation);
        return Instantiate(pickablePrefab,spawnLocation);
    }

    public void DestroyPickable(GameObject pickable)
    {
        Destroy(pickable);
    }
}
