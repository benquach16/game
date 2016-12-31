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
    public bool m_selected = false;
    public bool selected
    {
        get { return m_selected; }
        set { m_selected = value; }
    }
    public Projector m_projSelection;
    // Use this for initialization
    void Start () {
        m_projSelection = GetComponentInChildren<Projector>();
        m_projSelection.enabled = false;
    }
    
    // Update is called once per frame
    void Update () {
        //draw selection circle
        if (m_selected)
        {
            m_projSelection.enabled = true;
        }
    }
}
