using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class HybernationPotController : MonoBehaviour {
    [SerializeField]
    private bool m_PotIsMovable = false;
    [SerializeField]
    private GameObject m_LeftDoor;
    [SerializeField]
    private GameObject m_RightDoor;
    [SerializeField]
    private float m_PopSpeed;
    [SerializeField]
    private float m_PopDistance;
    [SerializeField]
    private float m_MoveSpeed;
    [SerializeField]
    private float m_MoveDistance;
    [SerializeField]
    private float m_HaltTime;
    [SerializeField]
    private Vector3 m_RelativeTargetPos = Vector3.zero;
    [SerializeField]
    private GameObject m_TargetPosIndicator;
    [SerializeField]
    private bool m_IndicatorVisible;
    // Use this for initialization

    private bool potOpen = false; 
    private Vector3 initialPos; 
    void Start() {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (m_PotIsMovable)
        {
            m_TargetPosIndicator.transform.position = transform.position + m_RelativeTargetPos;
            m_TargetPosIndicator.SetActive(m_IndicatorVisible);
        }
    }

    void OnCollisionStay(Collision col)
    {
        
        if (col.gameObject.tag == "ControlledPlayer")
        {
            if (Input.GetKeyDown(KeyCode.E) && m_PotIsMovable)
            {
                col.gameObject.GetComponent<CharacterInfo>().SetPushBox(true);
                Vector3 lookatPos = transform.position;
                lookatPos.y = col.gameObject.transform.position.y;
                col.gameObject.transform.LookAt(lookatPos);
                gameObject.transform.parent = col.gameObject.transform;
               
                /*
                Vector3 diff = transform.position - col.gameObject.transform.position;
                diff.y = 0f;
                diff.z = 0f;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + diff * Time.deltaTime, Time.deltaTime);
                if (Vector3.Distance(transform.position, initialPos + m_RelativeTargetPos) < 0.1f && m_RelativeTargetPos != Vector3.zero)
                {
                    m_PotIsMovable = false;
                    OpenPot();
                }*/

            }
            else if (Input.GetKeyDown(KeyCode.E) && !m_PotIsMovable && !potOpen)
            {
                OpenPot();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                col.gameObject.GetComponent<CharacterInfo>().SetPushBox(false);
                gameObject.transform.parent = null;
            }
            Debug.Log("Inside trigger stay loop");
        }
    }
    public void OpenPot()
    {
        potOpen = true; 
        StartCoroutine(OpenLeftPotDoor(m_HaltTime));
        StartCoroutine(OpenRightPotDoor(m_HaltTime));
    }
    IEnumerator OpenLeftPotDoor(float halt)
    {
        Vector3 LeftDoorTarget;
        LeftDoorTarget = m_LeftDoor.transform.position + m_LeftDoor.transform.forward * m_PopDistance;
        while (Vector3.Distance(m_LeftDoor.transform.position, LeftDoorTarget) > 0.01f)
        {
            m_LeftDoor.transform.position = Vector3.MoveTowards(m_LeftDoor.transform.position, LeftDoorTarget, m_PopSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(halt);
        LeftDoorTarget = m_LeftDoor.transform.position + m_LeftDoor.transform.right * m_MoveDistance;
        while (Vector3.Distance(m_LeftDoor.transform.position, LeftDoorTarget) > 0.01f)
        {
            m_LeftDoor.transform.position = Vector3.MoveTowards(m_LeftDoor.transform.position, LeftDoorTarget, m_MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator OpenRightPotDoor(float halt)
    {
        Vector3 RightDoorTarget;
        RightDoorTarget = m_RightDoor.transform.position + m_RightDoor.transform.forward * m_PopDistance;
        while (Vector3.Distance(m_RightDoor.transform.position, RightDoorTarget) > 0.01f)
        {
            m_RightDoor.transform.position = Vector3.MoveTowards(m_RightDoor.transform.position, RightDoorTarget, m_PopSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(halt);
        RightDoorTarget = m_RightDoor.transform.position - m_RightDoor.transform.right * m_MoveDistance;
        while (Vector3.Distance(m_RightDoor.transform.position, RightDoorTarget) > 0.01f)
        {
            m_RightDoor.transform.position = Vector3.MoveTowards(m_RightDoor.transform.position, RightDoorTarget, m_MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
