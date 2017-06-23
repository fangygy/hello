using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraForwardDistanceSet : MonoBehaviour {
    public float CameraForwardDistance = 5f;
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
#if UNITY_EDITOR
    
        Vector3 direction = transform.forward * CameraForwardDistance;
        Debug.DrawRay(transform.position, direction);
#endif
    }
    
}
