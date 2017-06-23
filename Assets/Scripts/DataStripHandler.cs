using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStripHandler : MonoBehaviour {
    [SerializeField]
    private Material NormalMat;
    [SerializeField]
    private Material HighLightMat;

    private Renderer m_Renderer;
    // Use this for initialization
    void Start() {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material = NormalMat;
    }

    // Update is called once per frame
    void Update() {

    }
    public void UseHighLightMat()
    {
        m_Renderer.material = HighLightMat;
    }
    public void UseNormalMat()
    {
        m_Renderer.material = NormalMat;
    }
}
