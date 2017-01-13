using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockstepManager : MonoBehaviour {
    static LockstepManager instance = null;
    public static LockstepManager getLockstepManager()
    {
        return instance;
    }
    private static float accumulatedTime = 0.0f;
    static float frameLength = 0.01f;
    // Use this for initialization
    private SceneManager m_sceneManager;

    private List<Command> m_commandsToSend;
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        m_sceneManager = gameObject.GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
        accumulatedTime += Time.deltaTime;
        while (accumulatedTime > frameLength)
        {
            simulate();
            //update here
            accumulatedTime -= frameLength;
        }
        //get inputs from other players and push
	}

    void simulate()
    {
        if (Player.getLocalPlayer())
            Player.getLocalPlayer().simulate();
        m_sceneManager.simulate();
    }

    void sendInputQueue()
    {
        //send and flush
        //serialize first
        
    }

    public void queueCommand(Command _command)
    {
        m_commandsToSend.Add(_command);
    }
}
