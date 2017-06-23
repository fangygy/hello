using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform t in transform)
        {
            t.gameObject.AddComponent<MeshCollider>();

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
