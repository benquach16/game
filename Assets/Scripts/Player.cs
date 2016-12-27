using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;


    void Start ()
    {
        transform.position = new Vector3(0, 5, -20);
        m_playerCamera = gameObject.GetComponent<Camera>();
        transform.LookAt(new Vector3(0, 0, 0));
        if(hasAuthority)
        {

            Debug.Log("Local player");
        }
    }
    
    void Update ()
    {
        if (!hasAuthority)
        {
            return;
        }
        //player controls go here
    }
}
