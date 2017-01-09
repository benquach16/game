using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockstepManager : MonoBehaviour {
    static LockstepManager instance = null;
    static LockstepManager getLockstepManager()
    {
        return instance;
    }
    private static float accumulatedTime = 0.0f;
    static float frameLength = 0.04f;
	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        accumulatedTime += Time.deltaTime;
        while (accumulatedTime > frameLength)
        {

            //update here
            accumulatedTime -= frameLength;
        }
	}
}
