using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviour {

    const int PORT = 27015;
    // Use this for initialization
    GameObject playerPrefab;
    uint m_numConnections = 2;
    NetworkMatch m_networkMatch;

    void Start () {
        m_networkMatch = gameObject.AddComponent<NetworkMatch>();
        //create match when we don't have any other games
        playerPrefab = Resources.Load("Prefabs/Objects/playerPrefab") as GameObject;
        m_networkMatch.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
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
            //since this is p2p we create a player here
            GameObject client = GameObject.Instantiate(playerPrefab);
        }
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            Debug.Log("Displayed Matches");
            Debug.Log(matches.Count + " matches found");
            //if no matches create a match
            if(matches.Count == 0)
            {
                m_networkMatch.CreateMatch("roomName", m_numConnections, true, "", "", "", 0, 0, OnMatchCreate);
                return;
            }

            foreach (MatchInfoSnapshot match in matches)
            {
                Debug.Log(match.name);
                //auto join if we have an open match
                if(match.currentSize < match.maxSize)
                {
                    m_networkMatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnJoinMatch);
                }
            }
        }
    }

    public void OnJoinMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("Joined Match");
        //do connection shit here
        //spawn a player for us
        GameObject client = GameObject.Instantiate(playerPrefab);
    }


    //deprecated for matchmaking
    //keeping this in case want to switch from matchmaking
    void StartServer()
    {
        NetworkServer.Listen(PORT);
    }
    public void SetupClient()
    {
        ClientScene.RegisterPrefab(playerPrefab);

        //client = new NetworkClient();
        //client.RegisterHandler(MsgType.Connect, OnConnected);
        //client.Connect("127.0.0.1", PORT);

    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
