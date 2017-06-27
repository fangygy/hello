using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterVirtual : MonoBehaviour {
	private GameObject security;
	private GameObject security2;
	private GameObject drone;
	private GameObject drone2;
	private GameManager gameManager;
	// Use this for initialization

	void Start () {
		security = GameObject.Find ("security");
		security2 = GameObject.Find ("security2");
		drone = GameObject.Find ("drone");
		drone2 = GameObject.Find ("drone2");
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
		
		if (other.gameObject.tag == "ControlledPlayer" && Input.GetKeyDown (KeyCode.E)) {
			//enter the virtual world
			gameManager.SwicthWorld();
			Debug.LogWarning("enter!");
			//change the world mat
			changeVirtualMat();

		}

	}

	void changeVirtualMat(){

	}

	void droneBlockDataFlow(){

	}
}
