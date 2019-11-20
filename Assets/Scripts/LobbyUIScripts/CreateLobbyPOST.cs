using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobbyPOST : MonoBehaviour
{
    InputField MaximumPlayers; 

    public void CreateLobby(string gameType, string isJoinable, int maxPlayers)
    {
        bool joinable; 
        if (isJoinable == "true")
        {
            joinable = true;
        }
        else
        {
            Debug.Log("hello"); 
            joinable = false; 
        }
   
        try
        {
            string webAddr = "http://lobbyservice.mooo.com:8080/lobbies/create";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                LobbyInfo newLobby = new LobbyInfo
                {
                    GameType = gameType,
                    Joinable = joinable,
                    MaximumPlayers = maxPlayers,

                };
                string json = LobbyInfo.CreateJSON(newLobby);
                Debug.Log(json); 
                //string json = "{\"GameType\":\"" + gameType + "\",\"" + "Joinable\":" + isJoinable + "," + "\"MaximumPlayers\":" + maxPlayers + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                Console.WriteLine(responseText);

                //Now you have your response.
                //or false depending on information in the response     
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


