using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class CreatePlayerMenu : MonoBehaviour
{
    public void CreatePlayer()
    {
        TMP_InputField playerName = GameObject.FindGameObjectWithTag("PlayerNameField").GetComponent<TMP_InputField>();
        if (playerName.text.Length > 0)
        {
            PlayerInfo newPlayer = new PlayerInfo();
            newPlayer.PlayerName = playerName.text;
            CreatePlayerPOST createPlayer = new CreatePlayerPOST();
            CurrentPlayer.curPlayer = createPlayer.CreatePlayer(newPlayer);

            LobbyManager.Instance.lobbyListMenu.SetActive(true);
            LobbyManager.Instance.MainMenu.SetActive(false);
            LobbyManager.Instance.createLobbyMenu.SetActive(false);
            LobbyManager.Instance.createPlayerMenu.SetActive(false);
            LobbyManager.Instance.inLobbyMenu.SetActive(false);
        }
    }
}
