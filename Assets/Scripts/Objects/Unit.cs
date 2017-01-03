using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Selectable
{
    //This is important so that we can map unique ids to objects accross the network
    //so these need to be in sync
    //should this be public?
    public static Dictionary<int, Unit> mapUnits = new Dictionary<int, Unit>();
    static int idCounter = 0;
    // Use this for initialization
    // Don't really need this since we can use unitys component system
    public List<GameObject> m_components;
    int m_currentId;
    public int currentId
    {
        get { return m_currentId; }
    }
    GameObject m_controllingPlayer;
    public GameObject controllingPlayer
    {
        get { return m_controllingPlayer; }
        set { m_controllingPlayer = value; }
    }

    Command m_currentCommand = null;
    public Command currentCommand
    {
        get { return m_currentCommand; }
        set { m_currentCommand = value; }
    }
    
    void Start ()
    {
        mapUnits.Add(idCounter, this);
        m_currentId = idCounter;
        idCounter++;

        tag = "Unit";
        GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1); //C#
        m_projSelection = GetComponentInChildren<Projector>();
        m_projSelection.enabled = false;
    }
    
    // Update is called once per frame
    public override void Update ()
    {
        base.Update();
        if(m_currentCommand != null)
        {
            doCommand();
        }
    }

    void doCommand()
    {
        var commandType = m_currentCommand.getType();
        switch(commandType)
        {
            case Command.E_TYPE.COMMAND_MOVE:
                {
                    CommandMove moveCmd = m_currentCommand as CommandMove;
                    var location = moveCmd.location;
                    transform.LookAt(location);
                    transform.Translate(new Vector3(0.1f, 0, 0));
                    break;
                }
        }

    }

    void runAI()
    {

    }

    private void OnGUI()
    {
        //render healthbar
    }

}
