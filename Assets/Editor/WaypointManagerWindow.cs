using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform m_waypointRoot = null;

    private void OnGUI()
    {
        SerializedObject windowObj = new SerializedObject(this);

        EditorGUILayout.PropertyField(windowObj.FindProperty("m_waypointRoot"));

        if(m_waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root must be selected.",MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }
        windowObj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + m_waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(m_waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if(m_waypointRoot.childCount > 1)
        {
            waypoint.m_previousWaypoint = m_waypointRoot.GetChild(m_waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.m_previousWaypoint.m_nextWaypoint = waypoint;

            //place the waypoint at the end of the last position.
            waypoint.transform.position = waypoint.m_previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.m_previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypointObject;
    }
}
