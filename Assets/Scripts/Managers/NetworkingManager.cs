﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;


public class NetworkingManager : NetworkManager {
    //there cant be two networkingmanagers
    static NetworkingManager instance = null;
    public static NetworkingManager getNetworkingManager()
    {
        return instance;
    }
    List<NetworkPlayer> m_connectedPlayers;

    List<Command> m_commandsToSend;
    NetworkClient m_client;
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
            m_client = StartHost(matchInfo);
            client.RegisterHandler(CommandMessageBuffer.msgType, CommandMsgEvent);
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
        m_client = StartClient(matchInfo);
        Debug.Log(matchInfo.address);
        client.Connect(matchInfo);
        client.RegisterHandler(MsgType.Connect, OnConnectedClient);
        client.RegisterHandler(CommandMessageBuffer.msgType, CommandMsgEvent);
    }

    public void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("player connected");
        m_connectedPlayers.Add(player);
        
    }

    //client only
    public void OnConnectedClient(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        //need to set connection as ready
        ClientScene.Ready(netMsg.conn);
        ClientScene.AddPlayer(0);

    }

    public void sendInputQueue()
    {
        //send and flush
        //serialize first
        //send to networking manager
        CommandMessageBuffer buf = new CommandMessageBuffer();
        buf.m_commands = m_commandsToSend.ToArray();
        client.SendByChannel(CommandMessageBuffer.msgType, buf, 1);
    }

    public void getInputQueue()
    {
        //read
        //only run this during the phases when we process input
        NetworkReader reader = new NetworkReader();
        List<Command> recvCommands = new List<Command>();


    }

    public void addCommand(Command _cmd)
    {
        m_commandsToSend.Add(_cmd);
    }

    //get a msg
    public void CommandMsgEvent(NetworkMessage _msg)
    {
        //deserialize cmd
        Debug.Log("STUFF");
    }


}
