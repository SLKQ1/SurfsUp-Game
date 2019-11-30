using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobbyPOST
{
    InputField MaximumPlayers;

    public LobbyInfo CreateLobby(string gameType, int maxPlayers)
    {
        string response = POSTLobby(gameType, maxPlayers);
        return LobbyInfo.CreateFromJSON(response); 
    }
 
 
    private string POSTLobby(string gameType, int maxPlayers)
    {
        try
        {
            string webAddr = JSONParser.url + "lobbies/create";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                if (gameType == "Solo")
                {
                    maxPlayers = 1; 
                }
                LobbyInfo newLobby = new LobbyInfo
                {
                    GameType = gameType,
                    MaximumPlayers = maxPlayers,

                };
                string json = LobbyInfo.CreateJSON(newLobby);

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();
                Console.WriteLine(responseText);
                return responseText; 
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Message);
            return ""; 
        }

    }
}


