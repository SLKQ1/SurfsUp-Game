using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using Mirror;

public class PortAndIP : MonoBehaviour
{
    public NetworkManager NetworkManagerScript; 
    // getting json for port and Ip
    public void Test()
    {
        int tempLobbyID = 1; 
        JSONParser PortIPParser = new JSONParser();
        ServerInfo Port_and_IP = PortIPParser.GetIPAndPORT(tempLobbyID);
        Debug.Log(Port_and_IP.IPAddress);
        Debug.Log(Port_and_IP.Port);

        // getting the IP and port from the NetworkObject
        var NetworkManagerScript = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<Transform>();
        //NetworkManagerScript = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<NetworkManager>();
        Debug.Log(NetworkManagerScript); 
    }

}
