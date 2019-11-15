using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    public Text curPlayersText;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;
    // var to store player id
    private int playerID; 
    // var to store players token
    private string token;
    public GameObject inLobbyCanvas; 

    //Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("UpdateCurrentPlayers", 0f, 5f);

    }

    //Update is called once per frame
    public void UpdateCurrentPlayers()
    {
        curPlayersText.text = "\n"; 
        // update the text to show all the current players and ready statuses
        if (lobbyID != 0)
        {
            // update if new player has joined, ready status of a player has changed
            int[] curPlayers = GetLobby(lobbyID).CurrentPlayers;

            for (int i = 0; i < curPlayers.Length; i++)
            {
                string curPlayerName = GetPlayer(curPlayers[i]).PlayerName;
                string curPlayerTeam = GetPlayer(curPlayers[i]).PlayerTeam;
                bool curPlayerStatus = GetPlayer(curPlayers[i]).PlayerReady;
                curPlayersText.text += curPlayerName + " " + curPlayerTeam + " " + curPlayerStatus + "\n";
            }

        }

    }

    public void SetLobbyID(int LobbyID)
    {
        LobbyIDText.text = "LobbyID: " + LobbyID.ToString();
        lobbyID = LobbyID;
    }

    public void SetToken(string playerToken)
    {
        token = playerToken;
    }
    public void SetPlayerID(int PlayerID)
    {
        playerID = PlayerID;
    }

    private LobbyInfo GetLobby(int LobbyID)
    {
        JSONParser JSONParser = new JSONParser();
        LobbyInfo curLobby = JSONParser.GetLobby(LobbyID);
        return curLobby; 
    }

    private PlayerInfo GetPlayer(int playerID)
    {
        JSONParser JSONParser = new JSONParser();
        PlayerInfo curPlayer = JSONParser.GetPlayer(playerID);
        return curPlayer; 
    }

    public void PatchReadyStatus(int playerID, string playerName, string playerTeam, bool playerReady)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080/players/" + playerID);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PATCH X";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            //{ "ID":17,"Token":"bn6qlh0lqqbcc5tr7tm0","LobbyID":3,"PlayerName":"Faiz","PlayerTeam":"red","PlayerReady":true}   
            //Debug.Log("{\"Token\":" + "\"" + token + ",\"" + "LobbyID\":" + lobbyID + ",\"" + "PlayerName\":" + "\"" + playerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "PlayerReady\":" + (!playerReady).ToString().ToLower() + "}");
            Debug.Log("{\"LobbyID\":" + lobbyID + "," + "\"PlayerName\":" + "\"" + playerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "," + "\"Token\":" + "\"" + token + "\"" + "," + "\"PlayerReady\":" + (!playerReady).ToString().ToLower() + "}");
            string json = "{\"LobbyID\":" + lobbyID + "," + "\"PlayerName\":" + "\"" + playerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "," + "\"Token\":" + "\"" + token + "\"" + "," + "\"PlayerReady\":" + (!playerReady).ToString().ToLower() + "}";
            streamWriter.Write(json);
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            // creating a player with the result
            PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
            //return NewPlayer;

        }


    }


    public void OnClick()
    {
        // getting cur player 
        int[] curPlayers = GetLobby(lobbyID).CurrentPlayers;
        int index = 0;
        for (int i = 0; i < curPlayers.Length; i++)
        {
            if (curPlayers[i] == playerID)
            {
                index = i; 
            }
        }
        PlayerInfo curPlayer = GetPlayer(curPlayers[index]);
        PatchReadyStatus(playerID, curPlayer.PlayerName, curPlayer.PlayerTeam, curPlayer.PlayerReady); 
    }
}