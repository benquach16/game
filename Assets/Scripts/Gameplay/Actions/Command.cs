using UnityEngine;
using System.Collections;

public class Command : MonoBehaviour {

    enum E_COMMAND_TYPES
    {
        COMMAND_ATTACK,
        COMMAND_STOP,
        COMMAND_MOVE
    }

    E_COMMAND_TYPES m_commandType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    E_COMMAND_TYPES getType()
    {
        return m_commandType;
    }
}
