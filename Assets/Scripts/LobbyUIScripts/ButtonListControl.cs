using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    // feild for buttons
    private GameObject[] gameObjects;
    // json parser 
    private JSONParser LobbyParser = new JSONParser();
    // storing old lobby size
    private List<LobbyInfo> prev_lobbyList;

    private void OnEnable()
    {
        DestroyAllButtons();
        GenButtons();
        InvokeRepeating("UpdateButtonList", 0f, 1f);

    }
    private void UpdateButtonList()
    {
        if (LobbyListChanged())
        {
            DestroyAllButtons();
            GenButtons();
        }
    }

    public void ButtonClicked(int LobbyID)
    {
        PlayerPatch newPlayerPatch = new PlayerPatch();
        // changing players lobby id 
        CurrentPlayer.curPlayer.LobbyID = LobbyID;
        newPlayerPatch.PatchPlayer(CurrentPlayer.curPlayer);

        // changing the panel to lobby instance
        LobbyManager.Instance.CreateInLobbyMenu();

    }

    // function to generate buttons
    void GenButtons()
    {
        List<LobbyInfo> LobbyList = LobbyParser.GetListLobbies();
        prev_lobbyList = LobbyList;

        // loop to create lobby buttons 
        for (int i = 0; i < LobbyList.Count; i++)
        {
            if (!LobbyList[i].IsStarted)
            {
                // making button and making visible
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.tag = "LobbyListButton";
                button.SetActive(true);

                // setting button text to be the lobby id and if it is started or not
                button.GetComponent<ButtonListButton>().SetText(LobbyList[i].ID, LobbyList[i].GameType + "\t" + LobbyList[i].CurrentPlayers.Length.ToString() + "/" + LobbyList[i].MaximumPlayers.ToString());
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
    private bool LobbyListChanged()
    {
        List<LobbyInfo> new_lobbyList = LobbyParser.GetListLobbies();
        if (prev_lobbyList.Count != new_lobbyList.Count)
        {
            return true;
        }
        return false;
    }
}
