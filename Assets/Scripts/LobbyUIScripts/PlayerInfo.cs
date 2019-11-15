﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerInfo 
{
    //    type player struct {

    //    ID int    `json:"ID"`
    //	Token string `json:"Token"`       // The Token of the player will serve as a secret between a player and the lobby service
    //	LobbyID int    `json:"LobbyID"`     // The lobby the player is currently joined. 0 if player is not in any lobby.
    //	PlayerName string `json:"PlayerName"`  // The name of the player shown in-game
    //	PlayerTeam string `json:"PlayerTeam"`  // The team name or team color the player is currently on.
    //	PlayerReady bool   `json:"PlayerReady"` // Whether or not the player is ready to play whilst in a lobby.
    //}
    public int ID { get; set; }
    public string Token { get; set; }
    public int LobbyID { get; set; }
    public string PlayerName { get; set; }
    public string PlayerTeam { get; set; }
    public bool PlayerReady { get; set; }

    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<PlayerInfo>(jsonString);
    }
    public static string CreateJSON(PlayerInfo cur_player)
    {
        return JsonConvert.SerializeObject(cur_player); 
    }

}
