using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    [SerializeField]
    MyCharacterController m_PhysicalCharacter;
    [SerializeField]
    MyCharacterController m_VirtualCharacter;
    [SerializeField]
    private bool IsInRealWorld = true;
    [SerializeField]
    private CameraFollow m_MainCameraControl;
    [SerializeField]
    private float SwitchWorldThreshold;
    [SerializeField]
    private GameObject AllLights;
    [SerializeField]
    private ScannerEffect m_Scan;
    public GameObject InvisibleObstacle;
    public static bool m_IsOnDataStrip = false;

    private float SwitchTriggerTimer = 0f;
    private DataStripHandler[] m_DataStrips;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(9, 12, true);
    }
    void Start()
    {
        m_DataStrips = GameObject.FindObjectsOfType<DataStripHandler>();
    }
	    
	// Update is called once per frame
	void Update () {
        SwitchTriggerTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (m_PhysicalCharacter.IsOnDataStrip() && IsInRealWorld)
            {
                if (SwitchTriggerTimer > SwitchWorldThreshold)
                {
               //     m_Scan.ScanOutward();
                    SwitchCharacter();
                }
            }
            else
            {

            }
        }
	}

    public void SwitchCharacter()
    {
        SwitchTriggerTimer = 0f;
        IsInRealWorld = !IsInRealWorld;
        m_VirtualCharacter.gameObject.SetActive(!IsInRealWorld);
        if (IsInRealWorld)
        {
            //   m_PhysicalCharacter.SetMoveEnabled(true);
            m_Scan.ScanInward();
            m_PhysicalCharacter.SetControlling(true);
            m_PhysicalCharacter.tag = "ControlledPlayer";
            m_MainCameraControl.SetPlayerReference(m_PhysicalCharacter.gameObject);
            AllLights.SetActive(true);
            foreach (DataStripHandler dsh in m_DataStrips)
            {
                dsh.UseNormalMat();
            }
        }
        else
        {
            m_Scan.ScanOutward();
            m_PhysicalCharacter.SetControlling(false);
            m_PhysicalCharacter.tag = "Player";
            m_VirtualCharacter.SetControlling(true);
            m_VirtualCharacter.transform.position = m_PhysicalCharacter.transform.position;
            m_VirtualCharacter.transform.rotation = m_PhysicalCharacter.transform.rotation;
            m_MainCameraControl.SetPlayerReference(m_VirtualCharacter.gameObject);
            AllLights.SetActive(false);
            foreach (DataStripHandler dsh in m_DataStrips)
            {
                dsh.UseHighLightMat();

            }
        }
    }
    public void SetPhysicalPlayer(GameObject go)
    {
        m_PhysicalCharacter = go.GetComponent<MyCharacterController>() ;
    }
    public GameObject GetPhysicalPlayer()
    {
            return m_PhysicalCharacter.gameObject;
    }   

    public void SetVirtualPlayer(GameObject go)
    {
        m_VirtualCharacter = go.GetComponent<MyCharacterController>();
    }
    public GameObject GetVirtualPlayer()
    {
        return m_VirtualCharacter.gameObject;
    }
    public bool InRealWorld()
    {
        return IsInRealWorld;
    }
}
