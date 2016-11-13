using UnityEngine;
using System.Collections;

public class Unit : Selectable
{

    // Use this for initialization
    //replace with components?

    enum E_ACTIONS
    {
        ACTION_STOP,
        ACTION_MOVE,
        ACTION_ATTACK,
    }

    E_ACTIONS m_currentCommand;

    Vector3 m_position;
    float m_rotation;

    void Start ()
    {
        m_currentCommand = E_ACTIONS.ACTION_STOP;
        m_position = new Vector3(0.0f, 0.0f, 0.0f);
        m_rotation = 0.0f;
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }
}
