using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;  
public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private void Start()
    {
        GenButtons(); 

    }

    // function to open lobby options when button is clicked 
    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
        // open the new canvas to show lobby joining options 
    }

    // function to generate buttons
    void GenButtons()
    {
        // testing to see if lobbies show
        JSONParser LobbyParser = new JSONParser();
        List<LobbyInfo> LobbyList = LobbyParser.GetListLobbies();
        Debug.Log("lobby: " + LobbyList[index: 1].ID);

        // loop to create lobby buttons 
        for (int i = 0; i < LobbyList.Count; i++)
        {
            // making button and making visible
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            // setting button text to be the lobby id and if it is started or not  
            button.GetComponent<ButtonListButton>().SetText("Lobby ID: " + LobbyList[i].ID + ", Is started: " + LobbyList[i].IsStarted);
            // setting button pos
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }


    }

}
