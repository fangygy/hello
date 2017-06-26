using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullControl : MonoBehaviour {
    [SerializeField]
    private Animator m_Anim;
	// Use this for initialization
	void Start () {
      //  m_Anim.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.O))
        {
            m_Anim.SetBool("PushingBoxes", true);
            m_Anim.SetFloat("PushFloat",0f);
        }
	}
}
