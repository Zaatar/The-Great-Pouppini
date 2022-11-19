using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.4f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Vector3 offset = waypoint.transform.right * waypoint.m_width / 2.0f;
        Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.transform.position - offset);

        if(waypoint.m_previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offsetTo = waypoint.m_previousWaypoint.transform.right * waypoint.m_previousWaypoint.m_width / 2.0f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.m_previousWaypoint.transform.position + offsetTo);
        }

        if(waypoint.m_nextWaypoint)
        {
            Gizmos.color = Color.green;
            Vector3 offsetTo = waypoint.m_nextWaypoint.transform.right * waypoint.m_nextWaypoint.m_width / 2.0f;

            Gizmos.DrawLine(waypoint.transform.position - offset, waypoint.m_nextWaypoint.transform.position - offsetTo);
        }
    }
}
