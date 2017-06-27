using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDectection : MonoBehaviour {
	public float droneMovementSpeed = 0.03f;
	public float yOffset = 2f;
	public float xOffset = 2f;
	private GameManager gameManager;
	private bool isTargetLocked;
	private GameObject target;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		isTargetLocked = false;
	}

	// Update is called once per frame
	void Update () {
		if (gameManager.InRealWorld ()) {
			droneMovementSpeed = 0.03f;
//			Vector3 original = new Vector3 (24.1f, -180f, 0f);
//			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, original, Time.deltaTime);
			
		} else {

			droneMovementSpeed = 0f;
//			Vector3 protectMode = new Vector3 (90f, -180f, 0f);
//			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, protectMode, Time.deltaTime);

		}

		if (isTargetLocked) {
			//target locked, call drone and security over
			GameObject drone = this.transform.parent.gameObject;
			Vector3 playersPos = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z);
			playersPos.y += yOffset;
			playersPos.x += xOffset;
			drone.transform.position = Vector3.MoveTowards (drone.transform.position, playersPos, droneMovementSpeed);

		} 
	}

	void OnTriggerStay(Collider other){
		if (!isTargetLocked) {
			//waiting and detecting
			if (other.gameObject.tag == "ControlledPlayer") {
				//drone now in place, maybe freeze the player here for a security scan;



				//found invader
				isTargetLocked = true;
				target = other.gameObject;
				commWithSecurity (other.gameObject);

			}
		}
	}

	void commWithSecurity(GameObject invader){
		GameObject security = GameObject.Find ("security");
		securityMovement script = security.GetComponent<securityMovement>();
		script.needSecurityHelp (invader);
		//GameObject security2 = GameObject.Find ("security2");
	}
}
