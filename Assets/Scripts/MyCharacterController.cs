using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(CharacterInfo))]
public class MyCharacterController : MonoBehaviour
{
    private CharacterInfo m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private bool m_OnDataStrip = false;
    private bool m_MoveEnabled=true;
    [SerializeField]
    private bool m_IsControlling = false;

    [SerializeField]
    private float dampingForce = 1f;

    private float HorizontalAxis = 0f;
    private float VerticalAxis = 0f;

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<CharacterInfo>();
    }


    private void Update()
    {
      
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        // read inputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        HorizontalAxis = Mathf.Lerp(HorizontalAxis, h, Time.deltaTime*dampingForce);
        VerticalAxis = Mathf.Lerp(VerticalAxis, v, Time.deltaTime * dampingForce);
        bool crouch = Input.GetKey(KeyCode.C);
        
        // calculate move direction to pass to character
  
            //     we use world-relative directions in the case of no main camera
        m_Move = VerticalAxis * Vector3.forward + HorizontalAxis * Vector3.right;
       
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        if (m_MoveEnabled && m_IsControlling)
        {
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (col.tag == "WorldSwitchTriggerArea"&&m_Character.IsPhysicalCharacter())
            {
                GameManager.Instance.SwitchCharacter();
            }
        }
        if (col.tag == "DataStrip")
        {
            m_OnDataStrip = true; 
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "DataStrip")
        {
            if (m_Character.IsPhysicalCharacter())
            {
                m_OnDataStrip = false;
            }
            else// (!m_Character.IsPhysicalCharacter())
            {
                //--------------Approach 1---------------//
                //--------------Spawn an invisible object in front of the character when character is trying to leave datastrip area-----------------//
                /*Vector3 SpawnPos = transform.position + transform.forward * 0.3f + transform.up * 0.7f;
                GameObject.Instantiate(GameManager.Instance.InvisibleObstacle, SpawnPos, transform.rotation);*/


                //--------------Approach 2-------------//
                //--------------Add force to push the player back when the player is trying to leave datastrip area-----------------//
                m_Character.GetRigidbody().AddForce(-transform.forward * 500f);
            }
        } 
    }
    void OnTriggerEnter(Collider col)   
    {
        if (col.tag == "WorldSwitchTriggerArea"&&!m_Character.IsPhysicalCharacter())
        {
            GameManager.Instance.SetPhysicalPlayer(col.gameObject.transform.parent.gameObject);
        }
       
    }

    public bool IsOnDataStrip()
    {
        return m_OnDataStrip;
    }
    public void SetMoveEnabled(bool move)
    {
        m_MoveEnabled = move; 
    }
    public void SetControlling(bool control)
    {
        m_IsControlling = control;
    }
    public bool IsControlling()
    {
        return m_IsControlling;
    }

}

