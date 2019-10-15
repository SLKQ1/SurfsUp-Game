using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { set; get; }

    // game objects for each screen on the menu
    public GameObject mainMenu;
    public GameObject lobbyListMenu;
    public GameObject hostGameMenu;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        lobbyListMenu.SetActive(false);
        hostGameMenu.SetActive(false); 
        DontDestroyOnLoad(gameObject); 
    }

    // host button
    public void HostButton()
    {
        Debug.Log("Host");
        mainMenu.SetActive(false);
        hostGameMenu.SetActive(true); 
    }

    // button to display lobbies
    public void LobbyListButton()
    {
        Debug.Log("list of lobbies");
        mainMenu.SetActive(false);
        lobbyListMenu.SetActive(true); 
    }

    // backbutton
    public void BackButton()
    {
        mainMenu.SetActive(true);
        lobbyListMenu.SetActive(false);
        hostGameMenu.SetActive(false); 

    }


}
