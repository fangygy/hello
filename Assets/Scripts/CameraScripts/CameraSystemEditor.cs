using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
[CustomEditor(typeof(KIIFCameraSystem))]
public class CameraSystemEditor :Editor{
    Vector3 SpawnPos;
    Vector3 LookAtPos;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        KIIFCameraSystem m_Cam = (KIIFCameraSystem)target;
        SpawnPos = EditorGUILayout.Vector3Field("Camera Spawn Position", SpawnPos);
        LookAtPos = EditorGUILayout.Vector3Field("Camera LookAt Position", LookAtPos);
        if (GUILayout.Button("Add Camera"))
        {
            m_Cam.AddCameraWayPoint(SpawnPos, LookAtPos);
        }
        m_Cam.CameraPosition.position = SpawnPos;
        m_Cam.CameraLookAtPosition.position = LookAtPos;
    }


  
}
