using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTarget : MonoBehaviour {

    public Rigidbody rb;

    public Vector3 pushpos;

    public GameObject player;
    public Vector3 player_dir;

    public float speed = 3.5f;

    public bool isMove;

    public bool isPushArea;

    public Animation door_anima;

    public float timer;
    public float inv = 1.0f;
    public bool isPlayAnima =true;



	// Use this for initialization
	void Start () {

        door_anima = transform.GetComponentInChildren<Animation>();
        this.rb.mass = 100000000;
	}
	
	// Update is called once per frame
	void Update () {

        TargetMove();
        if (gameObject.tag == "xiumiancang")
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -196.08f);
        }

        if (isPushArea)
        {
            Move();
        }

        if (transform.position == pushpos && isPlayAnima)
        {
            isPushArea = false;
            door_anima.PlayQueued("xiumiancang_door");
            door_anima.PlayQueued("xiumiancang_door2");
            isPlayAnima = false;

            rb.velocity = Vector3.zero;
            
            if (Time.realtimeSinceStartup - timer > inv)
            {
                
            }
        }

        if (transform.position == pushpos)
        {
            isMove = false;
            donmove = false;
        }
	}


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("进入");
        if (!donmove)
        {
            return;
        }
        if (other.gameObject.tag == "Player"&&isPushArea == false)
        {
            isMove = true;
           
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PushArea")
        {
            pushpos = other.transform.position;
            isMove = false;
            isPushArea = true;
            rb.velocity = Vector3.zero;
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PushArea")
        {
            //pushpos = other.transform.position;
            //isMove = false;
            //isPushArea = true;
            rb.velocity = Vector3.zero;
        }
    }

    public bool donmove;

    void OnCollisionExit(Collision other)
    {
        Debug.Log("退出");
        
        if (other.gameObject.tag == "Player")
        {
            donmove = true;
            isMove = false;

            rb.velocity = Vector3.zero;
        }
    }

    void TargetMove()
    {
        if (!donmove)
        {
            return;
        }

        if (transform.position.z - player.transform.position.z > 0.8f || transform.position.z - player.transform.position.z  < -0.8f)
        {
            return;
        }

        if (transform.position.x - player.transform.position.x >0)
        {
            player_dir = new Vector3(1, 0, 0);
        }
        else
        {
            player_dir = new Vector3(-1, 0, 0);
        }


       

        if (Input.GetKey(KeyCode.E)&&isMove)
        {
            Debug.Log("aaa");
            rb.velocity = player_dir * speed;
        }
        if (!Input.GetKey(KeyCode.E))
        {
            rb.velocity = Vector3.zero;
        }
       
    }

    

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, pushpos, Time.deltaTime * 1.5f);
        if (Vector3.Distance(transform.position,pushpos)<=0.2f)
        {
            Debug.Log("位置");
            transform.position = pushpos;
            timer = Time.realtimeSinceStartup;
        }

      
    }

    
}
