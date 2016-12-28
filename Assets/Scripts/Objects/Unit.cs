using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Selectable
{

    // Use this for initialization
    public List<GameObject> m_components;
    bool selected = false;

    void Start ()
    {

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
        selected = true;
    }
    void runAI()
    {

    }
}
