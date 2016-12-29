using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Selectable
{

    // Use this for initialization
    public List<GameObject> m_components;
    bool selected = false;
    Projector m_projSelection;
    void Start ()
    {
        
        GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1); //C#
        m_projSelection = GetComponent<Projector>();
        m_projSelection.enabled = false;
        
    }
    
    // Update is called once per frame
    void Update ()
    {
        //draw selection circle
        if(selected)
        {
            
        }
    }

    private void OnMouseDown()
    {
        //do selection
        Debug.Log("selected");
        selected = true;
        m_projSelection.enabled = true;
    }
    void runAI()
    {

    }
}
