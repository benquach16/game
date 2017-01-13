using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class Command : MessageBase{

    public static short msgType = 888;
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
