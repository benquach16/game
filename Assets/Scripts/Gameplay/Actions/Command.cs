using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


public abstract class Command {
    
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

    public List<int> m_ids;

    public virtual void serialize(NetworkWriter _writer)
    {
        BinaryFormatter bf = new BinaryFormatter();
        _writer.WritePackedUInt32((uint)m_commandType);
        
        
    }

    public virtual void deserialize(NetworkReader _reader)
    {

    }
}
