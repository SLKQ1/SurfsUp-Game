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

        // testing to see if lobbies show
        JSONParser LobbyParser = new JSONParser();
        List<LobbyInfo> LobbyList = LobbyParser.GetListLobbies(); 
        Debug.Log("lobby: " + LobbyList[index:1].ID);

        // loop to create lobby buttons 
        for (int i = 0; i < LobbyList.Count; i++)
        {
            // making button and making visible
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            // setting button text
            button.GetComponent<ButtonListButton>().SetText("Lobby ID: " + LobbyList[i].ID);
            // setting button pos
            button.transform.SetParent(buttonTemplate.transform.parent, false); 
        }

    }

    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }

    // function to generate buttons
    void GenButtons()
    {

    }

}
