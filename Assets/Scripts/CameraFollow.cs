using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private GameObject playerRef;
    [SerializeField]
    private float mCameraMoveSpeed = 5f;
    [SerializeField]
    private float mCameraRotateSpeed = 15f;
    [SerializeField]
    private float mCameraDragMultiplier = 3f;
    private Vector3 mCameraOffset;
    private float initialZValue;
    [SerializeField]
    private GameObject mLeadingCameras;
    // Use this for initialization
    private List<CameraNode> mCameraList;

    private CameraNode curCamNode;
   
    private Vector3 StableRandomCenter;
    //-----------------------a doubly linked node structure for setting up the camera curve------------------------------//
    class CameraNode
    {
        public CameraNode next;
        public CameraNode prev;
        public Vector3 trans;
        public Transform realTrans;
        public float forwardDistance;
        public CameraNode(CameraNode next, CameraNode prev, Vector3 trans,Transform realTrans,float forwardDistance)
        {
            this.next = next;
            this.prev = prev;
            this.trans = trans;
            this.realTrans = realTrans;
            this.forwardDistance = forwardDistance; 
        }
        public CameraNode(Transform realTrans)
        {
            this.next = null;
            this.prev = null;
            this.trans = Vector3.zero;
            this.realTrans = realTrans;
            this.forwardDistance = 5f;
        }
    }
	void Start () {
        mCameraOffset = transform.position - playerRef.transform.position;
        initialZValue = mCameraOffset.z;

        mCameraList = new List<CameraNode>();
        for (int i = 0; i < mLeadingCameras.transform.childCount;i++)
        {
            mCameraList.Add(new CameraNode(mLeadingCameras.transform.GetChild(i)));
            mCameraList[i].forwardDistance = mCameraList[i].realTrans.GetComponent<CameraForwardDistanceSet>().CameraForwardDistance;
            mCameraList[i].trans= mCameraList[i].realTrans.position + mCameraList[i].realTrans.forward*mCameraList[i].forwardDistance;
          
        }
        for (int i = 0; i < mCameraList.Count; i++)
        {
            if (i == 0)
            {
                mCameraList[i].prev = null;
            }
            else
            {
                mCameraList[i].prev = mCameraList[i - 1];
            }

            if (i == mCameraList.Count - 1)
            {
                mCameraList[i].next = null;
            }
            else
            { 
                mCameraList[i].next = mCameraList[i + 1];
            }
        }
        curCamNode = mCameraList[0];
        transform.position = curCamNode.realTrans.position;
        transform.rotation = curCamNode.realTrans.rotation;
    }

    // Update is called once per frame
    void FixedUpdate() {
        bool movingLeft = false;
        bool movingRight = false;
     
        float xVelocity = 0f;
        xVelocity = GameManager.Instance.GetPhysicalPlayer().GetComponent<Rigidbody>().velocity.x;
        if (xVelocity > 0.01f)
        {
            movingRight = true;
        }
        else if (xVelocity< -0.01f)
        {
            movingLeft = true;
        }
        if (movingRight || movingLeft)
        {
            MoveCamera(movingLeft, movingRight);
        }
        
    }
    void RandomizedMovement(float multiplier,Vector3 Center)
    {
       
    }
    void MoveCamera(bool movingLeft, bool movingRight)
    {
        if (movingLeft && movingRight)
        {
            //Do nothing now because player's stable   
        }
        else if (movingRight)
        {
            Vector3 pos;
            Quaternion rot;
            if (curCamNode.next != null)
            {
                float x = (playerRef.transform.position.x - curCamNode.trans.x) / (curCamNode.next.trans.x - curCamNode.trans.x);
                if (x > 0)  
                {
                    pos = Vector3.Lerp(curCamNode.realTrans.position, curCamNode.next.realTrans.position, x);
                    rot = Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.next.realTrans.rotation, x);
                    transform.position = Vector3.MoveTowards(transform.position,pos,mCameraMoveSpeed*Time.deltaTime+mCameraDragMultiplier*x*Time.fixedDeltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, mCameraRotateSpeed* Time.deltaTime) ;
                }
        
                else
                {
                    if(curCamNode.prev==null)
                         goto Start;
                    x = (curCamNode.trans.x - playerRef.transform.position.x) / (curCamNode.trans.x - curCamNode.prev.trans.x);
                    pos = Vector3.Lerp(curCamNode.realTrans.position, curCamNode.prev.realTrans.position, x);
                    rot = Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.prev.realTrans.rotation, x);
                    transform.position = Vector3.MoveTowards(transform.position, pos, mCameraMoveSpeed * Time.deltaTime + mCameraDragMultiplier * x*Time.fixedDeltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, mCameraRotateSpeed * Time.deltaTime);
                }
          
        
                if (x >= 1f||x<0f)
                {
                    curCamNode = curCamNode.next;
                }
                Start:
                float whataplaceholder;
            }
            else
            {
                float x = (curCamNode.trans.x - playerRef.transform.position.x) / (curCamNode.trans.x - curCamNode.prev.trans.x);
                transform.position =Vector3.MoveTowards(transform.position, Vector3.Lerp(curCamNode.realTrans.position, curCamNode.prev.realTrans.position, x), mCameraMoveSpeed * Time.deltaTime + mCameraDragMultiplier * x*Time.fixedDeltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.prev.realTrans.rotation, x),mCameraRotateSpeed*Time.deltaTime);
            }
        }
        else if (movingLeft)
        {
            if (curCamNode.prev != null)
            {
                float x = (curCamNode.trans.x - playerRef.transform.position.x) / (curCamNode.trans.x - curCamNode.prev.trans.x);
                if (x > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position,Vector3.Lerp(curCamNode.realTrans.position, curCamNode.prev.realTrans.position, x), mCameraMoveSpeed * Time.deltaTime + mCameraDragMultiplier * x*Time.fixedDeltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.prev.realTrans.rotation, x),mCameraRotateSpeed*Time.deltaTime);
                }
                else
                {
                    if (curCamNode.next == null)
                        goto SkipRest;
                    x = (playerRef.transform.position.x - curCamNode.trans.x) / (curCamNode.next.trans.x - curCamNode.trans.x);
                    transform.position = Vector3.MoveTowards(transform.position,Vector3.Lerp(curCamNode.realTrans.position, curCamNode.next.realTrans.position, x), mCameraMoveSpeed * Time.deltaTime + mCameraDragMultiplier * x*Time.fixedDeltaTime);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.next.realTrans.rotation, x),mCameraRotateSpeed*Time.deltaTime);

                }
                if (x >= 1f||x<0)
                {
                    curCamNode = curCamNode.prev;
                }
                SkipRest:
                float whataplaceholder;
            }
            else
            {
                float x = (playerRef.transform.position.x - curCamNode.trans.x) / (curCamNode.next.trans.x - curCamNode.trans.x);
                transform.position = Vector3.MoveTowards(transform.position,Vector3.Lerp(curCamNode.realTrans.position, curCamNode.next.realTrans.position, x), mCameraMoveSpeed * Time.deltaTime +mCameraDragMultiplier * x*Time.fixedDeltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Lerp(curCamNode.realTrans.rotation, curCamNode.next.realTrans.rotation, x),mCameraRotateSpeed*Time.deltaTime);
            }
        }
        else
        {

        }
    }
    public void SetPlayerReference(GameObject go)
    {
        playerRef = go;
    }
}
