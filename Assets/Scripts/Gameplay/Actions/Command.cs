using UnityEngine;
using System.Collections;


public class Command {

    public enum E_TYPE
    {
        COMMAND_ATTACK,
        COMMAND_STOP,
        COMMAND_MOVE
    }


    public E_TYPE m_commandType;

    public E_TYPE getType()
    {
        return m_commandType;
    }
}
