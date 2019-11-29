using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloGame : MonoBehaviour
{

    public static void SoloGameJoin()
    {

        // making lobby 
        CreateLobbyPOST creatingLobby = new CreateLobbyPOST();
        LobbyInfo newLobby = creatingLobby.CreateLobby("Solo", 1);
        Debug.Log("Lobby ID " + newLobby.ID); 
        // making player object  
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.LobbyID = newLobby.ID;
        newPlayer.PlayerReady = true;
        newPlayer.PlayerName = "Player";
        Debug.Log("Player ID: " + newPlayer.ID);
        // posting player
        CreatePlayerPOST creatingPlayer = new CreatePlayerPOST();
        creatingPlayer.CreatePlayer(newPlayer); 
        

        // setting ip and port 
        PortAndIP setPortAndIP = new PortAndIP();
        setPortAndIP.Set_Port_and_IP(newLobby.ID);

        // changing sence and starting client
        SceneManager.LoadScene(1);
        GameStateManager.maxPlayers = 1;
        Mirror.NetworkManager networkObject = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<Mirror.NetworkManager>();
        networkObject.StartClient();

    }
}
