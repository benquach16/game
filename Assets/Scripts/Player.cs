using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;

    GameObject Unit;

    void Start ()
    {
        Unit = Resources.Load("Prefabs/Units/Marine") as GameObject;
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
        //get commands issued then send to the client
        if(Input.anyKey)
        {
            var unit = GameObject.Instantiate(Unit);
            unit.transform.position = new Vector3();
        }
    }
}
