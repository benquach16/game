using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;

    public override void OnStartLocalPlayer() // this is our player
    {
        
        base.OnStartLocalPlayer();

        m_playerCamera = Camera.main;
        m_playerCamera.transform.position = new Vector3(0, 50, 0);
    }

    void Start ()
    {
        if(isClient)
        {

            Debug.Log("Local player");
        }
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
