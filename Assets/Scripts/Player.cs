using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;

    GameObject Unit;

    int[] m_selectedIds;

    InputHandler m_inputHandler;
    void Start ()
    {
        m_inputHandler = gameObject.GetComponent<InputHandler>();

        Unit = Resources.Load("Prefabs/Units/Marine") as GameObject;

        transform.position = new Vector3(0, 10, -10);
        m_playerCamera = gameObject.GetComponent<Camera>();
        transform.LookAt(new Vector3(0, 0, 0));
        if(hasAuthority)
        {
            Debug.Log("Local player");
        }
    }

    private void FixedUpdate()
    {


    }
    void Update ()
    {
        //dont run if we arent the player
        if (!hasAuthority)
        {
            return;
        }
        //player controls go here
        //get commands issued then send to the client
        if (Input.GetKey(KeyCode.A))
        {
            var unit = GameObject.Instantiate(Unit);
            unit.transform.position = new Vector3();
            var script = unit.GetComponent<Unit>();
            script.controllingPlayer = gameObject;
        }

        m_inputHandler.handleInput();

    }




    
}
