using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

//class for a selectable object
//abstract base class
public abstract class Selectable : MonoBehaviour {

    enum E_TEAM
    {
        TEAM1,
        TEAM2,
        TEAM_NEUTRAL
    }
    E_TEAM m_currentTeam;
    protected bool m_selected = false;
    [HideInInspector]
    public bool selected
    {
        get { return m_selected; }
        set { m_selected = value; }
    }
    protected Projector m_projSelection;
    // Use this for initialization
    void Start ()
    {

    }
    
    // Update is called once per frame
    public virtual void Update () {
        //draw selection circle
        if (m_selected)
        {
            m_projSelection.enabled = true;
        }
        else
            m_projSelection.enabled = false;
    }
}
