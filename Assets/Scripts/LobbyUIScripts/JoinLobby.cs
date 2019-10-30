using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobby : MonoBehaviour
{

    public InputField PlayerName;
    public InputField PlayerTeamColour;
    public string LobbyID;
    // private var to hold playerInfo var
    //private PlayerInfo NewPlayer; 

    public JoinLobby(string lobbyID)
    {
        LobbyID = lobbyID;
        //Debug.Log("Lobby ID is now: " + LobbyID);
    }
    //public string LobbyID { get; set; }

    // function to create a player by sending a post request to API
    // function then returns a new player of type PlayerInfo
    public static PlayerInfo CreatePlayer(string PlayerName, string PlayerTeam)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080//players/create");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            //'{"PlayerName": "Player 1", "PlayerTeam": "Green"}'
            string json = "{\"PlayerName\":" + "\"" + PlayerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + PlayerTeam + "\"" + "}";
            streamWriter.Write(json); 
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            // creating a player with the result
            PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
            return NewPlayer; 

        }
    }

    //function to join lobby with created player by sending patch request to API
    public void Join(PlayerInfo NewPlayer, string LobbyID)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080/players/" + NewPlayer.ID);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PATCH";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            //// if player is not in a lobby then updte
            //if (NewPlayer.LobbyID == 0)
            //{
            //    // update lobby id to lobby that was selected
            //    //{"LobbyID":1,"PlayerName":"Faiz","PlayerTeam":"Blue", "Token":"ur token"}
            //    string json = "{\"LobbyID\":" + LobbyID + "," + "\"PlayerName\":" + "\"" + NewPlayer.PlayerName + "\"," + "\"PlayerTeam\":" + "\"" + NewPlayer.PlayerTeam + "\"," + "\"Token\":" + "\"" + NewPlayer.Token + "\"" + "}";
            //    streamWriter.Write(json);
            //}

            // update lobby id to lobby that was selected
            //{"LobbyID":1,"PlayerName":"Faiz","PlayerTeam":"Blue", "Token":"ur token"}
            Debug.Log("{\"LobbyID\":" + LobbyID + "," + "\"PlayerName\":" + "\"" + NewPlayer.PlayerName + "\"," + "\"PlayerTeam\":" + "\"" + NewPlayer.PlayerTeam + "\"," + "\"Token\":" + "\"" + NewPlayer.Token + "\"" + "}");
            string json = "{\"LobbyID\":" + LobbyID + "," + "\"PlayerName\":" + "\"" + NewPlayer.PlayerName + "\"," + "\"PlayerTeam\":" + "\"" + NewPlayer.PlayerTeam + "\"," + "\"Token\":" + "\"" + NewPlayer.Token + "\"" + "}";
            streamWriter.Write(json);


        }
        //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //{
        //    var result = streamReader.ReadToEnd();
        //    // creating a player with the result
        //    PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
        //    return NewPlayer;

        //}

    }

    public void OnClick()
    {

        // creates a new player  
        PlayerInfo newPlayer = CreatePlayer(PlayerName.text, PlayerTeamColour.text);
        //Debug.Log("Player name: " + newPlayer.PlayerName);
        //Debug.Log("Player team: " + newPlayer.PlayerTeam);
        //Debug.Log("Player LobbyID: " + newPlayer.LobbyID);
        //Debug.Log("Player Token: " + newPlayer.Token);
        //this.LobbyID = "1"; 
        Debug.Log("Lobby ID is now: " + this.LobbyID);
        //Join(newPlayer, "1");
        Debug.Log("Player created in API");
        //int testlobbyid = 1;
        //string testplayername = "bob";
        //string testplayerteam = "blue";
        //string testplayertoken = "123"; 
        //Debug.Log("{\"LobbyID\":" + testlobbyid + "," + "\"PlayerName\":" + "\"" + testplayername + "\"," + "\"PlayerTeam\":" + "\"" + testplayerteam + "\"," + "\"Token\":" + "\"" + testplayertoken + "\"" + "}");
    }


}
