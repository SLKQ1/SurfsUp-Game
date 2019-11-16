using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

    public async Task PatchReadyStatusAsync(int playerID, string playerName, string playerTeam, bool playerReady, PlayerInfo curPlayer)
    {

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), "http://lobbyservice.mooo.com:8080/players/" + playerID))
            {
                // setting the player ready status to true 
                curPlayer.PlayerReady = !curPlayer.PlayerReady;
                string json = PlayerInfo.CreateJSON(curPlayer);
                Debug.Log(json); 

                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);
            }
        }


    }


    public void OnClick()
    {
        string name = this.currentPlayer.PlayerName;
        string team = this.currentPlayer.PlayerTeam;
        int id = this.currentPlayer.ID;
        bool isReady = this.currentPlayer.PlayerReady; 
        PatchReadyStatusAsync(id, name, team, isReady, this.currentPlayer); 
    }
}