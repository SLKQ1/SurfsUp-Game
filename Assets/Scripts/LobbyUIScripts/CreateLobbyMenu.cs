using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreateLobbyMenu : MonoBehaviour
{
    TextMeshProUGUI maxPlayersLabel;
    Slider maxPlayers;
    public void UpdateCount()
    {
        maxPlayersLabel = GameObject.FindGameObjectWithTag("MaxPlayerLabel").GetComponent<TextMeshProUGUI>();
        maxPlayers = GameObject.FindGameObjectWithTag("MaxPlayerSlider").GetComponent<Slider>();

        maxPlayersLabel.text = "Max Players: " + maxPlayers.value.ToString(); 
    }
    public async void CreateLobby()
    {
        TMP_InputField lobbyName = GameObject.FindGameObjectWithTag("LobbyNameField").GetComponent<TMP_InputField>();
        maxPlayersLabel = GameObject.FindGameObjectWithTag("MaxPlayerLabel").GetComponent<TextMeshProUGUI>();
        maxPlayers = GameObject.FindGameObjectWithTag("MaxPlayerSlider").GetComponent<Slider>();


        if (lobbyName.text.Length > 0) {
            CreateLobbyPOST lobbyPOST = new CreateLobbyPOST();
            LobbyInfo createdLobby = lobbyPOST.CreateLobby(lobbyName.text, (int)maxPlayers.value);

            Debug.Log(CurrentPlayer.curPlayer.LobbyID);

            CurrentPlayer.curPlayer.LobbyID = createdLobby.ID;
            PlayerPatch newPlayerPatch = new PlayerPatch();
            await newPlayerPatch.PatchPlayer(CurrentPlayer.curPlayer);

            Debug.Log(CurrentPlayer.curPlayer.LobbyID);
            // changing panel to be lobby instance
            LobbyManager.Instance.CreateInLobbyMenu();

        }


    }

}
