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
using UnityEngine.SceneManagement;

public class InLobby : MonoBehaviour
{
    public Text curPlayersText;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;

    public GameObject inLobbyCanvas;
    // var to store player info
    private PlayerInfo currentPlayer;
    // var to store player id array to minimize API calls
    private int[] curPlayers;
    // var to store lobby inorder to minimize API calls
    LobbyInfo curLobby;
    //public GameObject networkObject;
    //Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateCurrentPlayers", 0f, 1f);

    }
    private void Update()
    {
        if (AllPlayersReady())
        {
            Set_Port_and_IP(this.lobbyID);
			SceneManager.LoadScene(1);

            //networkObject.GetComponent<NetworkManagerHUD>().showGUI = true; 

        }
    }

    // method to update the current players in the lobby
    public void UpdateCurrentPlayers()
    {
        curPlayersText.text = "\n"; 
        // update the text to show all the current players and ready statuses
        if (lobbyID != 0)
        {
            // update if new player has joined, ready status of a player has changed
            this.curPlayers = GetLobby(lobbyID).CurrentPlayers;

            for (int i = 0; i < curPlayers.Length; i++)
            {
                PlayerInfo player = GetPlayer(curPlayers[i]); 
                string curPlayerName = player.PlayerName;
                //string curPlayerTeam = player.PlayerTeam;
                bool curPlayerStatus = player.PlayerReady;
                curPlayersText.text += "Player Name: " + curPlayerName + " ,Player Ready: " + curPlayerStatus + "\n";
            }

        }

    }

    // method to set lobby text and all feilds that require the lobby
    public void SetLobby(int LobbyID)
    {
        LobbyIDText.text = "LobbyID: " + LobbyID.ToString();
        lobbyID = LobbyID;
        // setting curlobby field 
        this.curLobby = GetLobby(this.lobbyID);
        // getting the current players in lobby 
        this.curPlayers = GetLobby(lobbyID).CurrentPlayers;

    }

    // method to set player
    public void SetPlayer(PlayerInfo cur_Player)
    {
        this.currentPlayer = cur_Player;
    }

    // method to get the current lobby from the API
    private LobbyInfo GetLobby(int LobbyID)
    {
        JSONParser JSONParser = new JSONParser();
        this.curLobby = JSONParser.GetLobby(LobbyID);
        return curLobby; 
    }

    // method to get the current player from the API
    private PlayerInfo GetPlayer(int player_ID)
    {
        JSONParser JSONParser = new JSONParser();
        PlayerInfo curPlayer = JSONParser.GetPlayer(player_ID);
        return curPlayer; 
    }

    // method to patch players ready status 
    private async Task PatchReadyStatusAsync(int playerID, string playerName, string playerTeam, bool playerReady, PlayerInfo curPlayer)
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

    // method to check if all players are ready 
    private bool AllPlayersReady()
    {
        // if all players are ready 
        if (curPlayers.Length == curLobby.MaximumPlayers)
        {
            int count = 0;
            for (int i = 0; i < curPlayers.Length; i++)
            {
                if (GetPlayer(curPlayers[i]).PlayerReady)
                {
                    count++;
                }
            }
            if (count == curLobby.MaximumPlayers)
            {
                return true; 
            }

        }
        return false; 

    }

    // method to set the port and IP
    private void Set_Port_and_IP(int LobbyID)
    {
        PortAndIP port_and_ip = new PortAndIP();
        port_and_ip.Set_Port_and_IP(LobbyID); 
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