using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class CreatePlayerPOST
{
    public PlayerInfo CreatePlayer(PlayerInfo newPlayer)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(JSONParser.url + "players/create");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {

            string json = PlayerInfo.CreateJSON(newPlayer);

  

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

}
