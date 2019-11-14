using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    public Text curPlayersText;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;
    // var to store players token
    private string token; 

    //Start is called before the first frame update

    //void Start()
    //{
    //    InvokeRepeating("GetLobby", 0f, 5f);

    //}

    //Update is called once per frame
    void Update()
    {
        curPlayersText.text = "\n"; 
        // update the text to show all the current players and ready statuses
        int[] curPlayers = GetLobby(lobbyID).CurrentPlayers;
        for (int i = 0; i < curPlayers.Length; i++)
        {
            string curPlayerName = GetPlayer(curPlayers[i]).PlayerName;
            string curPlayerTeam = GetPlayer(curPlayers[i]).PlayerTeam;
            bool curPlayerStatus = GetPlayer(curPlayers[i]).PlayerReady;
            curPlayersText.text += curPlayerName + " " + curPlayerTeam +" " + curPlayerStatus + "\n"; 
        }

        Debug.Log(token); 


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

    private void PatchReadyStatus()
    {

    }
}