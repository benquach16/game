using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void simulate()
    {
        foreach (var unit in Unit.mapUnits)
        {
            unit.Value.simulate();
        }

    }
}
