using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDectection : MonoBehaviour {
	public float droneMovementSpeed = 0.03f;
	public float droneRotationSpeed = 10f;
	public GameObject drone;
	private GameManager gameManager;
	private bool isTargetLocked;
	private GameObject target;
	private float originalY;
	private Light lightControl;
	// Use this for initialization
	void Start () {
		gameManager = GameManager.Instance;
		lightControl = GameObject.Find ("LightControl").GetComponent<Light> ();
		isTargetLocked = false;
		originalY = drone.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (isTargetLocked) {
			//target locked, call drone and security over
			Vector3 playersPos = new Vector3 (target.transform.position.x, originalY, target.transform.position.z);
			float dist = Vector3.Distance (playersPos, drone.transform.position);
			if(dist >  2f){
				//move towards target
				drone.transform.position = Vector3.MoveTowards (drone.transform.position, playersPos, droneMovementSpeed*Time.deltaTime);
//				drone.transform.LookAt (target.transform);
//				Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - drone.transform.position);
//				drone.transform.rotation = Quaternion.Slerp (drone.transform.rotation, targetRotation, droneRotationSpeed*Time.deltaTime);

				//drone faces to the player, fix rotation on Y axis
				Vector3 targetPostition = new Vector3( target.transform.position.x, drone.transform.position.y, target.transform.position.z ) ;
				drone.transform.LookAt(targetPostition);
//				drone.transform.eulerAngles = new Vector3 (0, drone.transform.eulerAngles.y, 0);
			}
		} 
	}

	void OnTriggerStay(Collider other){
		if (!isTargetLocked && lightControl.intensity > 1f && gameManager.InRealWorld()) {
			//waiting and detecting
			if (other.gameObject.tag == "ControlledPlayer") {
				//drone now in place, maybe freeze the player here for a security scan;



				//found invader
				isTargetLocked = true;
				blockDataStrip ();
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

	void blockDataStrip(){
		gameManager.setDataStripStatus (true);
	}
}
