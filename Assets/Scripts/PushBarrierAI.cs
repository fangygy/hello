using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBarrierAI : MonoBehaviour {
    [SerializeField]
    private CharacterInfo m_Info;
    [SerializeField]
    private MyCharacterController m_Controller;
    [SerializeField]
    private BarrierDriver Barrier;
    private bool IsPushingBarrier;
    // Use this for initialization
    void Start() {
        if (m_Info == null)
        {
            m_Info = GetComponent<CharacterInfo>();
        }
        if (m_Controller == null)
        {
            m_Controller = GetComponent<MyCharacterController>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (!m_Controller.IsControlling())
        {
            if (IsPushingBarrier)
            {
                m_Info.Move(transform.forward, false, false);
                Barrier.transform.position += transform.forward * Time.deltaTime;
            }
        }
        else
        {
            IsPushingBarrier = false;
        }
    }
    public void PushBarrier()
    {
        IsPushingBarrier = true;
    }

    public BarrierDriver GetBarrier()
    {
        return Barrier;
    }
}
