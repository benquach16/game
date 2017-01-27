using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CommandMessageBuffer : MessageBase {
    public static short msgType = 889;
    public Command[] m_commands;
}
