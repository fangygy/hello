using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDectection : MonoBehaviour {
	public float droneMovementSpeed = 0.03f;
	public float droneRotationSpeed = 10f;
	public float yOffset = 2f;
	public float xOffset = 2f;
	public GameObject drone;
	private GameManager gameManager;
	private bool isTargetLocked;
	private GameObject target;
	private float originalY;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.Instance;
		isTargetLocked = false;
		originalY = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		
		if (isTargetLocked) {
			//target locked, call drone and security over
			Vector3 playersPos = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z);
			playersPos.y = originalY + yOffset;
			playersPos.x += xOffset;
			drone.transform.position = Vector3.MoveTowards (drone.transform.position, playersPos, droneMovementSpeed*Time.deltaTime);
		//	drone.transform.LookAt (target.transform);
//			Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - drone.transform.position);
//			drone.transform.rotation = Quaternion.Slerp (drone.transform.rotation, targetRotation, droneRotationSpeed*Time.deltaTime);
//			drone.transform.LookAt (target.transform);
//			drone.transform.eulerAngles = new Vector3 (0, drone.transform.eulerAngles.y, 0);
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
		GameObject security2 = GameObject.Find ("security2");
		securityMovement script = security.GetComponent<securityMovement>();
		securityMovement script2 = security2.GetComponent<securityMovement>();

		script.needSecurityHelp (invader);
		script2.needSecurityHelp (invader);
		//GameObject security2 = GameObject.Find ("security2");
	}
}
