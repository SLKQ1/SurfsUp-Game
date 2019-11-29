using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { set; get; }

    // game objects for each screen on the menu
    //public GameObject createPlayerMenu; 
    public GameObject MainMenu;
    public GameObject lobbyListMenu;
    public GameObject createLobbyMenu;
    public GameObject inLobbyMenu;
    public GameObject createPlayerMenu; 

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        MainMenu.SetActive(true); 
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        DontDestroyOnLoad(gameObject); 
    }

    public void CreatePlayerMenu()
    {
        MainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(true);
        inLobbyMenu.SetActive(false);

    }

    public void StartSoloGame()
    {
        SoloGame.SoloGameJoin(); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }





}
