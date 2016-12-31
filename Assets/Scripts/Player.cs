using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    Camera m_playerCamera;

    //temporary
    GameObject marine;

    List<int> m_selectedIds;

    InputHandler m_inputHandler;
    void Start ()
    {
        m_inputHandler = gameObject.GetComponent<InputHandler>();

        marine = Resources.Load("Prefabs/Units/Marine") as GameObject;

        transform.position = new Vector3(0, 10, -10);
        m_playerCamera = gameObject.GetComponent<Camera>();
        transform.LookAt(new Vector3(0, 0, 0));
        if(hasAuthority)
        {
            Debug.Log("Local player");
        }
    }
    public void setSelectedObjs(List<int> objectIds)
    {
        m_selectedIds = objectIds;
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
            var unit = GameObject.Instantiate(marine);
            unit.transform.position = new Vector3();
            var script = unit.GetComponent<Unit>();
            script.controllingPlayer = gameObject;
        }
        if(Input.GetMouseButtonDown(1))
        {

            foreach (int id in m_selectedIds)
            {
                Unit.mapUnits[id].currentCommand = new CommandMove(new Vector3(10,-1,10));
            }
        }
        m_inputHandler.handleInput();

    }

    void sendOrder(Command.E_TYPE _command)
    {

    }


    
}
