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
    public GameObject createControlsMenu;


    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        MainMenu.SetActive(true); 
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        createControlsMenu.SetActive(false);
        DontDestroyOnLoad(gameObject); 
    }

    public void CreateInLobbyMenu()
    {
        MainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        createControlsMenu.SetActive(false);
        inLobbyMenu.SetActive(true);

    }

    public void CreateLobbyMenu()
    {
        MainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(true);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        createControlsMenu.SetActive(false);
    }

    public void CreateLobbyListMenu()
    {
        MainMenu.SetActive(false);
        lobbyListMenu.SetActive(true);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        createControlsMenu.SetActive(false);
    }

    public void CreateControlsMenu()
    {
        MainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        createControlsMenu.SetActive(true);

    }


    public void CreatePlayerMenu()
    {
        // checking if players already exists 
        if (CurrentPlayer.curPlayer == null)
        {
            MainMenu.SetActive(false);
            lobbyListMenu.SetActive(false);
            createLobbyMenu.SetActive(false);
            createPlayerMenu.SetActive(true);
            inLobbyMenu.SetActive(false);
            createControlsMenu.SetActive(false);
        }
        else
        {

            CreateLobbyListMenu();
        }

    }

    public void StartSoloGame()
    {

        // making lobby 
        CreateLobbyPOST creatingLobby = new CreateLobbyPOST();
        LobbyInfo newLobby = creatingLobby.CreateLobby("Solo", 1);
        // making player object  
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.LobbyID = newLobby.ID;
        newPlayer.PlayerReady = true;
        newPlayer.PlayerName = "Player";
        // posting player
        CreatePlayerPOST creatingPlayer = new CreatePlayerPOST();
        creatingPlayer.CreatePlayer(newPlayer);

        PlayerInfo.PlayerNameStatic = newPlayer.PlayerName;


        // setting ip and port 
        PortAndIP setPortAndIP = new PortAndIP();
        setPortAndIP.Set_Port_and_IP(newLobby.ID);

        StartCoroutine(JoinGame());
    }

    IEnumerator JoinGame()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        // changing sence and starting client
        //SceneManager.LoadScene(1);
        GameStateManager.maxPlayers = 1;
        Mirror.NetworkManager networkObject = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<Mirror.NetworkManager>();
        networkObject.StartClient();

    }

    public void BackTOMainMenu()
    {
        MainMenu.SetActive(true);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        createPlayerMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        createControlsMenu.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit(); 
    }





}
