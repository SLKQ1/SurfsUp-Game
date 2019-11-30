using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI; 
public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    //public Text LobbyIDText;
    // ref to join lobby canvas
    public JoinLobby joinLobby;
    private GameObject[] gameObjects;

    private void Start()
    {
        GenButtons();
        InvokeRepeating("DestroyAllButtons", 0f, 5f); 

    }
    private void LateUpdate()
    {
        DestroyAllButtons();
        GenButtons();
    }

    public async void ButtonClicked(int LobbyID)
    {
        PlayerPatch newPlayerPatch = new PlayerPatch();
        // changing players lobby id 
        CurrentPlayer.curPlayer.LobbyID = LobbyID;
        await newPlayerPatch.PatchPlayer(CurrentPlayer.curPlayer);

        // changing the panel to lobby instance 
        LobbyManager.Instance.MainMenu.SetActive(false);
        LobbyManager.Instance.lobbyListMenu.SetActive(false);
        LobbyManager.Instance.createLobbyMenu.SetActive(false);
        LobbyManager.Instance.createPlayerMenu.SetActive(false);
        LobbyManager.Instance.inLobbyMenu.SetActive(true);

    }

    // function to generate buttons
    void GenButtons()
    {
        // testing to see if lobbies show
        JSONParser LobbyParser = new JSONParser();
        List<LobbyInfo> LobbyList = LobbyParser.GetListLobbies();
        //Debug.Log("lobby: " + LobbyList[index: 0].ID);

        // loop to create lobby buttons 
        for (int i = 0; i < LobbyList.Count; i++)
        {
            if (!LobbyList[i].IsStarted) {
                // making button and making visible
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.tag = "LobbyListButton"; 
                button.SetActive(true);

                // setting button text to be the lobby id and if it is started or not
                button.GetComponent<ButtonListButton>().SetText(LobbyList[i].ID,LobbyList[i].GameType + "\t" +LobbyList[i].CurrentPlayers.Length.ToString() + "/" + LobbyList[i].MaximumPlayers.ToString());
                // setting button pos
                button.transform.SetParent(buttonTemplate.transform.parent, false);

            }
        }

    }
    void DestroyAllButtons()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("LobbyListButton");


        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
