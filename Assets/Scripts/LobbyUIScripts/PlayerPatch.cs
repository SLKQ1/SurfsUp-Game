using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPatch
{
    public async Task PatchPlayer(PlayerInfo curPlayer)
    {

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), JSONParser.url + "players/" + curPlayer.ID))
            {

                string json = PlayerInfo.CreateJSON(curPlayer);
                Debug.Log(json);

                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);
            }
        }


    }
}
