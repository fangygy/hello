using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Destroy_door()
    {

    }

    void OnTriggerStay(Collider other)
    {
       
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("破坏门");
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
