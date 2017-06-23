using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public Vector3 dir;

    public float moveSpeed = 3.5f;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        Move(rb);
	}

    void Move(Rigidbody current)
    {
        if (Input.GetKey(KeyCode.W))
        {
            dir = Vector3.back;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            dir = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.zero;
        }



        current.velocity = dir * moveSpeed;

    }
    //void FixedUpdate()
    //{
    //    rb.velocity = new Vector3(0, 0, 0);
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "qiehuan")
        {
            Debug.Log("切换");
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Change cg = other.gameObject.GetComponent<Change>();
                rb = cg.rb;
            }
            
        }
    }
}
