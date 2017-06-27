using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityMovement : MonoBehaviour {
	public float securityMovementSpeed = 0.5f;
	public float securityRotationSpeed = 1f;
	public float attackRange = 2.0f;
	public Transform checkPos;
	public Transform guardPos;
	private bool isInvaded = false;
	private GameObject targetToClear;
	private Animator anim;
	private GameManager gameManager;
	private float originalY;
	private Light lightControl;
	private float timeCheck = 3f;
	private bool isReturn = false;
	private bool inFix = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameManager = GameManager.Instance;
		lightControl = GameObject.Find ("LightControl").GetComponent<Light> ();
		originalY = transform.position.y;
		updateAnimation (0);
	}	
   
	// Update is called once per frame
	void Update () {
		if (isInvaded) {
			//found invader

			float Distance = Vector3.Distance (transform.position, targetToClear.transform.position);

			if (Distance > attackRange) {
				//chase the target
				Vector3 targetPos = new Vector3 (targetToClear.transform.position.x, originalY, targetToClear.transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, targetPos, securityMovementSpeed * Time.deltaTime);

				//look at the target
				//method 1 quaternion.lookrotation
//				Quaternion targetRotation = Quaternion.LookRotation(targetToClear.transform.position - transform.position);
//				Vector3 cross = Vector3.Cross(transform.rotation*Vector3.forward, targetRotation*Vector3.forward);
//
//				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, securityRotationSpeed*Time.deltaTime);
				//method 2 lookat
				transform.LookAt (targetToClear.transform);
				transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

				updateAnimation (1);
				//if has the turn animation, following lines of code may be needed
				//if (cross.y < 0) {
				//right turn
				//updateAniation (1.0f, -0.7f);
				//} else {
				//left turn
				//updateAniation (1.0f, 0.7f);
				//}

			} else {
				//face enemy & attack
				transform.LookAt (targetToClear.transform);
				transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
				updateAnimation (2);
			}
		}
		else if(inFix || (!isInvaded && lightControl.intensity < 1f)){
			//light is off, check power node
			float powerDistance = Vector3.Distance (transform.position, checkPos.position);
			if (powerDistance > 1f) {
				//run towards power node
				Vector3 targetPos2 = new Vector3 (checkPos.position.x, originalY, checkPos.position.z);
				transform.position = Vector3.MoveTowards (transform.position, targetPos2, securityMovementSpeed * Time.deltaTime);
				//face the direction
//				Quaternion targetR = Quaternion.LookRotation(checkPos.position - transform.position);
//				transform.rotation = Quaternion.Slerp (transform.rotation, targetR, securityRotationSpeed*Time.deltaTime);

				//transform.LookAt (checkPos);
				//transform.localEulerAngles = new Vector3 (0, checkPos.localEulerAngles.y, 0);
				Quaternion q = Quaternion.Euler(new Vector3 (0, checkPos.localEulerAngles.y, 0));

				transform.rotation = Quaternion.Lerp(transform.rotation, q, securityRotationSpeed*Time.deltaTime);

				updateAnimation (1);

			} else {
				//check power node, may need other animations here
				inFix = true;
				updateAnimation (0);
				timeCheck -= Time.deltaTime;
				//Debug.Log ("time left: " + timeCheck);
				if (timeCheck < 0.01f || lightControl.intensity > 3f) {
					//light back to normal
					//Debug.Log("prep to return");
					lightControl.intensity = 3.32f;
					isReturn = true;
					timeCheck = 3f;
					inFix = false;
					//face the origin
					transform.LookAt(guardPos);
				}
			}
		} 

		else if(!isInvaded && lightControl.intensity > 3f && isReturn == true){
			//return to the guard point
			Debug.Log("in return loop");
			float guardDistance = Vector3.Distance (transform.position, guardPos.position);
			Debug.Log ("distance:" + guardDistance);
			if (guardDistance < 0.01f) {
				//returned
				Debug.Log("returned");
				updateAnimation (0);
				isReturn = false;

			} else {
				//move back 
				Debug.Log("move back");
				Vector3 targetPos3 = new Vector3 (guardPos.position.x, originalY, guardPos.position.z);
				transform.position = Vector3.MoveTowards (transform.position, targetPos3, securityMovementSpeed * Time.deltaTime);

				updateAnimation (1);
			}

		}
		else {
			//no special case, guard the gate
			updateAnimation (0);

		}
	}

	void updateAnimation(int state){
		if (state == 0) {
			//idle
			anim.ResetTrigger("isRun");
			anim.ResetTrigger ("isFire");
			anim.SetTrigger("isIdle");

		} else if (state == 1) {
			//run
			anim.ResetTrigger("isFire");
			anim.ResetTrigger ("isIdle");
			anim.SetTrigger("isRun");

		} else if (state == 2) {
			//Fire
			anim.ResetTrigger("isRun");
			anim.ResetTrigger ("isIdle");
			anim.SetTrigger("isFire");
		}

	}

	void freezeOrMove(){
		if (gameManager.InRealWorld ()) {
			
			securityMovementSpeed = 0.5f;
			securityRotationSpeed = 1.0f;

		} else {

			securityMovementSpeed = 0.0f;
			securityRotationSpeed = 0.0f;

		}
	}

	public void needSecurityHelp(GameObject invader){

		isInvaded = true;
		targetToClear = invader;
	}
}
