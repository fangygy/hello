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
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
   
	// Update is called once per frame
	void Update () {
		//Debug.Log ("security awaken");
		freezeOrMove();

		if (isInvaded) {
			float Distance = Vector3.Distance (transform.position, targetToClear.transform.position);
			Debug.LogWarning (Distance);
			if(Distance > attackRange){
				//chase the target
				transform.position = Vector3.MoveTowards(transform.position, targetToClear.transform.position, securityMovementSpeed*Time.deltaTime);
				//look at the target
				Quaternion targetRotation = Quaternion.LookRotation(targetToClear.transform.position - transform.position);
				Vector3 cross = Vector3.Cross(transform.rotation*Vector3.forward, targetRotation*Vector3.forward);

				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, securityRotationSpeed);

				//if (cross.y < 0) {
					//right turn
					//updateAniation (1.0f, -0.7f);
				//} else {
					//left turn
					//updateAniation (1.0f, 0.7f);
				//}
			}
			else{
				//attack

				//updateAniation (0f, 0f);
			}
		}
	}

//	void updateAniation(float forward_amt, float turn_amt){
//		
//		anim.SetFloat("Forward", forward_amt, 0.1f, Time.deltaTime);
//		anim.SetFloat("Turn", turn_amt, 0.1f, Time.deltaTime);
//
//	}
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
