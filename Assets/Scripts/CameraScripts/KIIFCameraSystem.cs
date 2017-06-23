using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KIIFCameraSystem : MonoBehaviour {
    [SerializeField]
    private Transform focalPoint;
    [SerializeField]
    private Transform playerRef;
    [SerializeField]
    private Camera TemplateGuideCamera;
    [SerializeField]
    private GameObject LeadingCameras;

    public Transform CameraPosition;
    public Transform CameraLookAtPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

    public void AddCameraWayPoint(Vector3 pos,Vector3 LookAtPos)
    {
        Camera NextCamera = GameObject.Instantiate(TemplateGuideCamera, pos, Quaternion.Euler(LookAtPos-pos), LeadingCameras.transform);
        NextCamera.transform.LookAt(LookAtPos);
        NextCamera.gameObject.AddComponent<CameraForwardDistanceSet>();
    }
  
}
