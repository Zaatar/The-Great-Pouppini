using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject[] humansPrefabs;
    public int HumansToSpawn;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHumans());
    }

    IEnumerator SpawnHumans()
    {
        count = 0;
        while(count < HumansToSpawn)
        {
            GameObject obj = Instantiate(humansPrefabs[Random.Range(0, humansPrefabs.Length)]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<HumanNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.GetComponent<HumanNavigator>().IsReversed = Random.Range(0, 10)%2 == 0;
            obj.transform.position = child.position;

            yield return new WaitForFixedUpdate();

            count++;
        }
    }
}
