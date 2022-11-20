using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject[] humansPrefabs;
    List<Transform> selectedWaypoints = new List<Transform>();
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
            Transform child;
            do
            {
               child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            } while (selectedWaypoints.Contains(child));

            selectedWaypoints.Add(child);

            if (obj.GetComponent<HumanNavigator>() != null)
                obj.GetComponent<HumanNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            else
                obj.GetComponent<CarNavigator>().currentWaypoint = child.GetComponent<Waypoint>();

            obj.transform.position = child.position;

            yield return new WaitForFixedUpdate();

            count++;
        }
    }
}
