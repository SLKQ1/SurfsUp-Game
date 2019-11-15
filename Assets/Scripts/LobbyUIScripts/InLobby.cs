using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    public Text curPlayersText;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;

    public GameObject inLobbyCanvas;
    // var to store player info
    private PlayerInfo currentPlayer; 

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

    public void SetPlayer(PlayerInfo cur_Player)
    {
        currentPlayer = cur_Player; 
    }

    private LobbyInfo GetLobby(int LobbyID)
    {
        JSONParser JSONParser = new JSONParser();
        LobbyInfo curLobby = JSONParser.GetLobby(LobbyID);
        return curLobby; 
    }

    private PlayerInfo GetPlayer(int player_ID)
    {
        JSONParser JSONParser = new JSONParser();
        PlayerInfo curPlayer = JSONParser.GetPlayer(player_ID);
        return curPlayer; 
    }

    public void PatchReadyStatus(int playerID, string playerName, string playerTeam, bool playerReady, PlayerInfo curPlayer)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080/players/" + playerID);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PATCH X";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            curPlayer.PlayerReady.Equals(true);
            string json = PlayerInfo.CreateJSON(curPlayer);

            Debug.Log(json);

            //Debug.Log("{\"LobbyID\":" + lobbyID + "," + "\"PlayerName\":" + "\"" + playerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "," + "\"Token\":" + "\"" + token + "\"" + "," + "\"PlayerReady\":" + (!playerReady).ToString().ToLower() + "}");
            //string json = "{\"LobbyID\":" + lobbyID + "," + "\"PlayerName\":" + "\"" + playerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "," + "\"Token\":" + "\"" + token + "\"" + "," + "\"PlayerReady\":" + true + "}";
            //string json = "{\"LobbyID\":" + 1 + "," + "\"PlayerName\":" + "\"" + "" + "\"" + "," + "\"PlayerTeam\":" + "\"" + playerTeam + "\"" + "," + "\"Token\":" + "\"" + token + "\"" + "," + "\"PlayerReady\":" + true + "}";
            streamWriter.Write(json);
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //Debug.Log(httpResponse); 
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Debug.Log(result); 
            // creating a player with the result
            PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
            //return NewPlayer;

        }


    }


    public void OnClick()
    {
        string name = this.currentPlayer.PlayerName;
        string team = this.currentPlayer.PlayerTeam;
        int id = this.currentPlayer.ID;
        bool isReady = this.currentPlayer.PlayerReady; 
        PatchReadyStatus(id, name, team, isReady, this.currentPlayer); 
    }
}