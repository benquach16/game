using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviour {

    const int PORT = 27015;
    // Use this for initialization
    NetworkClient client;
    GameObject playerPrefab;
    int m_numConnections = 2;
    bool m_isServer;

    NetworkMatch networkMatch;
    void Start () {
        m_isServer = false;
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        networkMatch.CreateMatch("roomName", 4, true, "", "", "", 0, 0, OnMatchCreate);

    }

    void StartServer()
    {
        NetworkServer.Listen(PORT);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if(success)
        {
            Debug.Log("Created Match");
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
            NetworkServer.Listen(matchInfo, matchInfo.port);
            networkMatch.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
        }
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            Debug.Log("Displayed Matches");
            Debug.Log(matches.Count);
            //if no matches create a match
            for(int i = 0; i < matches.Count; i++)
            {

            }
        }
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        ClientScene.RegisterPrefab(playerPrefab);

        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.Connect("127.0.0.1", PORT);

    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
