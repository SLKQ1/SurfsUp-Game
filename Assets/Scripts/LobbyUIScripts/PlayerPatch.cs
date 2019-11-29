using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPatch : MonoBehaviour
{
    public async Task PatchPlayer(PlayerInfo curPlayer)
    {

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), "http://lobbyservice.mooo.com:8080/players/" + curPlayer.ID))
            {
                // setting the player ready status to true 
                curPlayer.PlayerReady = !curPlayer.PlayerReady;
                string json = PlayerInfo.CreateJSON(curPlayer);
                Debug.Log(json);

                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);
            }
            //GameObject.FindGameObjectWithTag("NetworkObject").SendMessage("SetPlayerName", playerName); 
        }


    }
}
