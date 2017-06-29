using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakPowerNode : MonoBehaviour {
	private Light lightControl;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		lightControl = GameObject.Find ("LightControl").GetComponent<Light>();
		gameManager = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
		//turn off the light, here note the virtual character is tagged with player
		if (other.gameObject.tag == "Player" && !gameManager.InRealWorld() && Input.GetKeyDown(KeyCode.E)) {
			Debug.Log ("enter trigger");
			lightControl.intensity = 0f;

		}
	}
}
