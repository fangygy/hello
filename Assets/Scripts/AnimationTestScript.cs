using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour {
    [SerializeField]
    private Animator m_Animator; 
	// Use this for initialization
	void Start () {
        if (m_Animator == null)
        {
            m_Animator = GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
        
	}
}
