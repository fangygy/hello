using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityMovement : MonoBehaviour {
	public float securityMovementSpeed = 0.5f;
	public float securityRotationSpeed = 1f;
	public float attackRange = 2.0f;
	private bool isInvaded = false;
	private GameObject targetToClear;
	private Animator anim;
	private GameManager gameManager;
	private float originalY;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameManager = GameManager.Instance;
		originalY = transform.position.y;
		updateAniation (0);
	}	
   
	// Update is called once per frame
	void Update () {
		//Debug.Log ("security awaken");

		if (isInvaded) {
			float Distance = Vector3.Distance (transform.position, targetToClear.transform.position);
			//	Debug.LogWarning (Distance);
			if (Distance > attackRange) {
				//chase the target
				Vector3 targetPos = new Vector3 (targetToClear.transform.position.x, originalY, targetToClear.transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, targetPos, securityMovementSpeed * Time.deltaTime);

				//look at the target
//				Quaternion targetRotation = Quaternion.LookRotation(targetToClear.transform.position - transform.position);
//				Vector3 cross = Vector3.Cross(transform.rotation*Vector3.forward, targetRotation*Vector3.forward);
//
//				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, securityRotationSpeed*Time.deltaTime);

				transform.LookAt (targetToClear.transform);
				transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

				updateAniation (1);
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
				updateAniation (2);
			}
		} else {

			updateAniation (0);

		}
	}

	void updateAniation(int state){
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
