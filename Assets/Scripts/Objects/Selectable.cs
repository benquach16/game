using UnityEngine;
using System.Collections;

//class for a selectable object
public class Selectable : MonoBehaviour {

    enum E_TEAM
    {
        TEAM1,
        TEAM2,
        TEAM_NEUTRAL
    }
    E_TEAM m_currentTeam;

    Vector3 m_position;
    float m_rotation;
    // Use this for initialization
    void Start () {
        m_position = new Vector3(0.0f, 0.0f, 0.0f);
        m_rotation = 0.0f;

    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
