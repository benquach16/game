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
    protected int m_currentId;
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

    //TODO: refactor me!!
    UnitComponent[] m_components = new UnitComponent[(int)UnitComponent.E_COMPONENT_TYPES.SIZE];
    void Start ()
    {
       
        m_components[(int)UnitComponent.E_COMPONENT_TYPES.E_TYPE_MOVEMENT] = GetComponent<Movement>() as UnitComponent;
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
    }

    public void simulate()
    {
        if (m_currentCommand != null)
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
                    //see if we have movement component then defer action to the movement component
                    ((Movement)m_components[(int)UnitComponent.E_COMPONENT_TYPES.E_TYPE_MOVEMENT]).move(location, gameObject);
                    break;
                }
            case Command.E_TYPE.COMMAND_STOP:
                {
                    //stop everything
                    break;
                }
        }

    }

    void runAI()
    {

    }

    private void OnGUI()
    {
        //TODO: refactor me :)
        if (Player.getLocalPlayer())

        {
            var screenPos = Player.getLocalCamera().WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            GUI.Label(new Rect(screenPos.x - 20, screenPos.y - 20, screenPos.x + 20, screenPos.y + 20), "Sphess Muhreen");
        }


    }

}
