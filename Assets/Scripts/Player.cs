using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;

    public override void OnStartLocalPlayer() // this is our player
    {
        base.OnStartLocalPlayer();

        m_playerCamera = new Camera();
    }

    void Start ()
    {
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
