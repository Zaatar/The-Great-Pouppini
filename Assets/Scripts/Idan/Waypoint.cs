using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint m_previousWaypoint = null;
    public Waypoint m_nextWaypoint = null;
    
    [Range(0.0f, 5.0f)]
    public float m_width = 1.0f;
    //helpers
    private Transform m_cacheTransform = null;
    private void Start()
    {
        m_cacheTransform = transform;
    }
    /// <summary>
    /// Gives a random location along the waypoint width range.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPosition()
    {
        Vector3 widthRange = m_cacheTransform.right * m_width / 2.0f;

        Vector3 minBounds = m_cacheTransform.position + widthRange;
        Vector3 maxBounds = m_cacheTransform.position - widthRange;

        return Vector3.Lerp(minBounds, maxBounds, Random.Range(0.0f, 1.0f));
    }
}
