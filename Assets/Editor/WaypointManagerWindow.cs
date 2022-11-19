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
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if(GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if(GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if(GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    private void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if(selectedWaypoint.m_nextWaypoint != null)
        {
            selectedWaypoint.m_nextWaypoint.m_previousWaypoint = selectedWaypoint.m_previousWaypoint;
        }
        if(selectedWaypoint.m_previousWaypoint)
        {
            selectedWaypoint.m_previousWaypoint.m_nextWaypoint = selectedWaypoint.m_nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.m_previousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);

        foreach (Transform waypoint in m_waypointRoot)
        {
            waypoint.name = "Waypoint " + waypoint.GetSiblingIndex();
        }
    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + m_waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(m_waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.m_previousWaypoint = selectedWaypoint;

        if(selectedWaypoint.m_nextWaypoint != null)
        {
            selectedWaypoint.m_nextWaypoint.m_previousWaypoint = newWaypoint;
            newWaypoint.m_nextWaypoint = selectedWaypoint.m_nextWaypoint;
        }

        selectedWaypoint.m_nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        foreach (Transform waypoint in m_waypointRoot)
        {
            waypoint.name = "Waypoint " + waypoint.GetSiblingIndex();
        }
        Selection.activeGameObject = waypointObject;
    }

    private void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + m_waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(m_waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.m_previousWaypoint != null)
        {
            newWaypoint.m_previousWaypoint = selectedWaypoint.m_previousWaypoint;
            selectedWaypoint.m_previousWaypoint.m_nextWaypoint = newWaypoint;
        }

        newWaypoint.m_nextWaypoint = selectedWaypoint;
        selectedWaypoint.m_previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        foreach (Transform waypoint in m_waypointRoot)
        {
            waypoint.name = "Waypoint " + waypoint.GetSiblingIndex();
        }
        Selection.activeGameObject = waypointObject;
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
