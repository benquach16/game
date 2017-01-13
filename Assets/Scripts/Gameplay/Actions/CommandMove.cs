using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandMove : Command
{

    Vector3 m_location;
    List<int> m_ids;
    public Vector3 location
    {
        get { return m_location; }
        set { m_location = value; }
    }
    // Use this for initialization
    public CommandMove(Vector3 _location, List<int> _ids)
    {
        m_commandType = E_TYPE.COMMAND_MOVE;
        m_location = _location;
        m_ids = _ids;

        foreach (int id in _ids)
        {
            Unit.mapUnits[id].currentCommand = this;
        }
    }



}
