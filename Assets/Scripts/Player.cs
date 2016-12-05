using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;
    void Start ()
    {
        //create a camera for the player
        m_playerCamera = new Camera();
    }
    
    void Update ()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        //player controls go here
    }
}
