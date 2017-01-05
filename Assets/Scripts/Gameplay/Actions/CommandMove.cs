using UnityEngine;
using System.Collections;

public class CommandMove : Command
{

    Vector3 m_location;
    public Vector3 location
    {
        get { return m_location; }
        set { m_location = value; }
    }
    // Use this for initialization
    public CommandMove(Vector3 _location)
    {
        m_commandType = E_TYPE.COMMAND_MOVE;
        m_location = _location;
    }



}
