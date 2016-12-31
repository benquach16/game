using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Selectable
{

    // Use this for initialization
    public List<GameObject> m_components;

    GameObject m_controllingPlayer;
    public GameObject controllingPlayer
    {
        get { return m_controllingPlayer; }
        set { m_controllingPlayer = value; }
    }
    void Start ()
    {
        tag = "Unit";
        GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1); //C#

    }
    
    // Update is called once per frame
    void Update ()
    {

    }

    void runAI()
    {

    }

    
}
