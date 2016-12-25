using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    // Use this for initialization
    ArrayList m_playerList;
    NetworkClient client;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   

    // Create a client and connect to the server port
    public void SetupClient()
    {
        ClientScene.RegisterPrefab(Player);

        myClient = new NetworkClient();

        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
    }
}
