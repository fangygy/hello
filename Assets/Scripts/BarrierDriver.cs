using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDriver: MonoBehaviour {
    
    [SerializeField]
    private bool IsInvisible= false;
    [SerializeField]
    private PushBarrierAI BarrierPusher;
    [SerializeField]
    private float BarrierExpansionSpeed=1f;
   
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ControlledPlayer")
        {
            if (IsInvisible)
            {
                ShowBarrier();
                if (BarrierPusher != null)
                {
                    BarrierPusher.GetBarrier().ShowBarrier();
                }
            }
            if (BarrierPusher != null)
            {
                BarrierPusher.PushBarrier();
            }
        }
    }

    public void ShowBarrier()
    {
        StartCoroutine(ExpandBarrier());
    }
    IEnumerator ExpandBarrier()
    {
        Vector3 targetScale = Vector3.one;
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, BarrierExpansionSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PushArea")
        {
            col.gameObject.layer = 9;
            col.rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
