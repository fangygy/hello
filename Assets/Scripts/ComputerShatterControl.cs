using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerShatterControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform t in transform)
        {
            Rigidbody rgbd = t.gameObject.AddComponent<Rigidbody>();
            BoxCollider boxcol=  t.gameObject.AddComponent<BoxCollider>();
            rgbd.constraints = RigidbodyConstraints.FreezeAll;
            boxcol.size = new Vector3(boxcol.size.x / 1.2f, boxcol.size.y / 1.2f, boxcol.size.z / 1.2f);
            t.gameObject.tag = "PushArea";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (Transform t in transform)
            {
                Rigidbody rgbd = t.gameObject.GetComponent<Rigidbody>();
                rgbd.isKinematic = false;
            }
        }
	}
}
