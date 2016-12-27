using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;


public class NetworkingManager : NetworkManager {

    const int PORT = 25001;
    // Use this for initialization
    GameObject playerPrefab;
    uint m_numConnections = 2;
    NetworkMatch m_networkMatch;
    void Start () {
        StartMatchMaker();

        m_networkMatch = gameObject.AddComponent<NetworkMatch>();
        //create match when we don't have any other games
        playerPrefab = Resources.Load("Prefabs/Objects/playerPrefab") as GameObject;
        matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }


	// Update is called once per frame
	void Update () {
	
	}

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if(success)
        {
            Debug.Log("Created Match");
            Debug.Log(matchInfo.address);
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
            StartHost(matchInfo);

            //since this is p2p we create a player here
            //NetworkServer.Spawn(playerPrefab);

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
                //use local host for now

                matchMaker.CreateMatch("roomName", m_numConnections, true, "", "127.0.0.1", "", 0, 0, OnMatchCreate);
                return;
            }

            foreach (MatchInfoSnapshot match in matches)
            {
                Debug.Log(match.name);
                //auto join if we have an open match
                if(match.currentSize < match.maxSize)
                {
                    matchMaker.JoinMatch(match.networkId, "", "127.0.0.1", "", 0, 0, OnJoinMatch);
                    return;
                }
            }
        }
    }

    public void OnJoinMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("Got Match");
        //do connection shit here
        //spawn a player for us
        if (Utility.GetAccessTokenForNetwork(matchInfo.networkId) == null)
            Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);

        var client = StartClient(matchInfo);
        Debug.Log(matchInfo.address);
        client.Connect(matchInfo.address, matchInfo.port);
        client.RegisterHandler(MsgType.Connect, OnConnected);

        if (!isNetworkActive)
            Debug.Log("sdf");

    }

    public void test(NetworkMessage msg)
    {
        Debug.Log("getmessage");
    }

    //deprecated for matchmaking
    //keeping this in case want to switch from matchmaking

    public void SetupClient()
    {
        ClientScene.RegisterPrefab(playerPrefab);

        //client = new NetworkClient();
        //client.RegisterHandler(MsgType.Connect, OnConnected);
        //client.Connect("127.0.0.1", PORT);
        MasterServer.PollHostList();

    }
    public void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("connection done");
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        //GameObject player = (GameObject)GameObject.Instantiate((Object)playerPrefab);
        client.Send(1002, new IntegerMessage(5));
    }
}
