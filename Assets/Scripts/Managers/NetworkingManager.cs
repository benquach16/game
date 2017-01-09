using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;


public class NetworkingManager : NetworkManager {
    //there cant be two networkingmanagers
    static NetworkingManager instance = null;
    static NetworkingManager getNetworkingManager()
    {
        return instance;
    }

    const int PORT = 25001;
    void Start () {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        StartMatchMaker();
        //create match when we don't have any other games
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
            var client = StartHost(matchInfo);
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

                matchMaker.CreateMatch("roomName", (uint)maxConnections, true, "", "127.0.0.1", "", 0, 0, OnMatchCreate);
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
        client.Connect(matchInfo);
        client.RegisterHandler(MsgType.Connect, OnConnectedClient);
    }

    public void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("player connected");
    }
    public void OnConnectedClient(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        //need to set connection as ready
        ClientScene.Ready(netMsg.conn);
        ClientScene.AddPlayer(0);
    }


}
