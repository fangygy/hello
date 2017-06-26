using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDectection : MonoBehaviour {
	public float droneMovementSpeed = 0.03f;
	public float yOffset = 2f;
	public float xOffset = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "ControlledPlayer") {
			//call drone over
			GameObject drone = this.transform.parent.gameObject;
			Vector3 playersPos = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
			playersPos.y += yOffset;
			playersPos.x += xOffset;
			drone.transform.position = Vector3.MoveTowards (drone.transform.position, playersPos, droneMovementSpeed);

			//drone now in place, maybe freeze the player here for a security scan;



			//found invader
			commWithSecurity(other.gameObject);

		}
	}

	void commWithSecurity(GameObject invader){
		GameObject security = GameObject.Find ("security");
		securityMovement script = security.GetComponent<securityMovement>();
		script.needSecurityHelp (invader);
		//GameObject security2 = GameObject.Find ("security2");
	}
}
