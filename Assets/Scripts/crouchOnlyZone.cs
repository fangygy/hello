using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchOnlyZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "ControlledPlayer") {
			CharacterInfo script = other.gameObject.GetComponent<CharacterInfo> ();
			script.setCrouchZone (true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "ControlledPlayer") {
			CharacterInfo script = other.gameObject.GetComponent<CharacterInfo> ();
			script.setCrouchZone (false);
		}
	}
}
