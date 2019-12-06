using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InLobbyMenu : MonoBehaviour
{
    TextMeshProUGUI currentPlayersInLobbyText;
    TextMeshProUGUI currentLobbyText;

    public void OnEnable()
    {
        currentPlayersInLobbyText = GameObject.FindGameObjectWithTag("CurrentPlayersInLobby").GetComponent<TextMeshProUGUI>();
        currentLobbyText = GameObject.FindGameObjectWithTag("LobbyNameText").GetComponent<TextMeshProUGUI>();

        if ((CurrentPlayer.curPlayer != null) && (CurrentPlayer.curPlayer.LobbyID != 0))
        {
            currentLobbyText.text = "Lobby: " + new JSONParser().GetLobby(CurrentPlayer.curPlayer.LobbyID).GameType;
        }
        InvokeRepeating("UpdatePlayersInLobby", 0f, 1f);
    }

    IEnumerator JoinGame(LobbyInfo Lobby)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        // changing sence and starting client
        //SceneManager.LoadScene(1);
        GameStateManager.maxPlayers = Lobby.CurrentPlayers.Length;

        Mirror.NetworkManager networkObject = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<Mirror.NetworkManager>();
        networkObject.StartClient();

    }

    private void UpdatePlayersInLobby()
    {
        if ((CurrentPlayer.curPlayer != null) && (CurrentPlayer.curPlayer.LobbyID != 0))
        {
            JSONParser LobbyParser = new JSONParser();
            LobbyInfo Lobby = LobbyParser.GetLobby(CurrentPlayer.curPlayer.LobbyID);

            if (AllPlayersReady(Lobby))
            {
                PortAndIP port_and_ip = new PortAndIP();
                port_and_ip.Set_Port_and_IP(Lobby.ID);
                //Mirror.NetworkClient.Connect(NetworkManagerScript.networkAddress);
                StartCoroutine(JoinGame(Lobby));

                LobbyManager.Instance.MainMenu.SetActive(false);
                LobbyManager.Instance.lobbyListMenu.SetActive(false);
                LobbyManager.Instance.createLobbyMenu.SetActive(false);
                LobbyManager.Instance.createPlayerMenu.SetActive(false);
                LobbyManager.Instance.inLobbyMenu.SetActive(false);
                LobbyManager.Instance.createControlsMenu.SetActive(false);
                LobbyManager.Instance.loadingScreen.SetActive(true);
            }
            else
            {
                currentPlayersInLobbyText.text = "";
                int readyPlayers = 0;
                for (int i = 0; i < Lobby.CurrentPlayers.Length; i++)
                {
                    int playerID = Lobby.CurrentPlayers[i];
                    PlayerInfo aPlayer = LobbyParser.GetPlayer(playerID);

                    string isReady = "Not Ready";
                    if (aPlayer.PlayerReady)
                    {
                        readyPlayers++;
                        isReady = "Ready";
                    }
                    currentPlayersInLobbyText.text += "\nPlayer Name: " + aPlayer.PlayerName + "\nPlayer Ready: " + isReady + "\n";

                }
            }

        }

    }

    public async void PlayerLeavePatch()
    {
        PlayerPatch newPlayerPatch = new PlayerPatch();
        // changing players lobby id 
        CurrentPlayer.curPlayer.LobbyID = 0;
        // changing playrs ready status
        CurrentPlayer.curPlayer.PlayerReady = false; 
        await newPlayerPatch.PatchPlayer(CurrentPlayer.curPlayer);

		// changing the panel to lobby instance
		LobbyManager.Instance.CreateLobbyListMenu(); 

    }
    public async void PlayerReady()
    {
        PlayerPatch newPlayerPatch = new PlayerPatch();
        // changing players lobby id 
        CurrentPlayer.curPlayer.PlayerReady = !CurrentPlayer.curPlayer.PlayerReady;
        await newPlayerPatch.PatchPlayer(CurrentPlayer.curPlayer);

    }

    // method to check if all players are ready 
    private bool AllPlayersReady(LobbyInfo lobby)
    {

        // if all players are ready 
        if (lobby.CurrentPlayers.Length == lobby.MaximumPlayers)
        {
            int count = 0;
            for (int i = 0; i < lobby.CurrentPlayers.Length; i++)
            {
                if (new JSONParser().GetPlayer(lobby.CurrentPlayers[i]).PlayerReady)
                {
                    count++;
                }
            }
            if (count == lobby.MaximumPlayers)
            {
                return true;
            }

        }
        return false;

    }

    async void PlayerLeaveLobby()
    {
        CurrentPlayer.curPlayer.LobbyID = 0;
        CurrentPlayer.curPlayer.PlayerReady = false;
        await new PlayerPatch().PatchPlayer(CurrentPlayer.curPlayer);
    }
    void OnApplicationQuit()
    {
        this.PlayerLeaveLobby();
    }

}
