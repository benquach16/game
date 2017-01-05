﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    static Player m_localPlayer = null;
    static List<Player> m_networkPlayers;
    public static Player getLocalPlayer()
    {
        return m_localPlayer;
    }
    public static Camera getLocalCamera()
    {
        if (m_localPlayer != null)
            return m_localPlayer.m_playerCamera;
        else
            return null;
    }
    Camera m_playerCamera;
    public Camera playerCamera
    {
        get { return m_playerCamera; }
    }
    //temporary
    GameObject marine;

    protected List<int> m_selectedIds = new List<int>();
    protected InputHandler m_inputHandler;

    private static float accumulatedTime = 0.0f;
    private static float frameLength = 0.03f;
    private static int gameFrame = 0;
    void Start ()
    {
        m_inputHandler = gameObject.GetComponent<InputHandler>();

        marine = Resources.Load("Prefabs/Units/Marine") as GameObject;

        transform.position = new Vector3(0, 20, -10);
        m_playerCamera = gameObject.GetComponent<Camera>();
        transform.LookAt(new Vector3(0, 0, 0));
        if(hasAuthority)
        {
            Debug.Log("Local player");
            m_localPlayer = this;
        }
    }
    public void setSelectedObjs(List<int> objectIds)
    {
        m_selectedIds = objectIds;
    }
    public void clearSelectedObjs()
    {
        foreach(int id in m_selectedIds)
        {
            Unit.mapUnits[id].selected = false;
        }
        m_selectedIds.Clear();
    }

    void Update ()
    {
        accumulatedTime += Time.deltaTime;
        //dont run if we arent the player
        if (!hasAuthority)
        {
            return;
        }
        while(accumulatedTime > frameLength)
        {
            gameTurn();
            accumulatedTime -= frameLength;
        }

    }

    void gameTurn()
    {
        //player controls go here
        //get commands issued then send to the client
        if (Input.GetKey(KeyCode.A))
        {
            var unit = GameObject.Instantiate(marine);
            unit.transform.position = new Vector3();
            var script = unit.GetComponent<Unit>();
            script.controllingPlayer = gameObject;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray.origin, ray.direction, out hit, 100);
            var point = hit.point;
            point.y = 0;
            foreach (int id in m_selectedIds)
            {
                Unit.mapUnits[id].currentCommand = new CommandMove(point);
            }
        }
        m_inputHandler.handleInput();

    }

    void sendOrder(Command.E_TYPE _command)
    {

    }


    
}