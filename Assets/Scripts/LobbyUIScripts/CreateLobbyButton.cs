using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobbyButton : MonoBehaviour
{
    List<string> gameTypeOptions = new List<string>() { "Please select", "Solo", "Multiplayer" };
    List<string> joinableOptions = new List<string>() { "Please select", "true", "false" };

    public InputField MaxPlayers;
    public Dropdown gameTypeDropdown;
    public Dropdown joinableDropdown;

    // vars for the selected options
    public string gameType;
    public string isJoinable;
    //string maxPlayers;


    public void Dropdown_IndexChanged_GameType(int index)
    {
        Debug.Log(gameTypeOptions[index]);
        gameType = gameTypeOptions[index];
    }
    public void Dropdown_IndexChanged_JoinableOptions(int index)
    {
        Debug.Log(joinableOptions[index]);
        isJoinable = joinableOptions[index]; 
    }

    private void Start()
    {
        PopulateLists();
    }

    void PopulateLists()
    {
        gameTypeDropdown.AddOptions(gameTypeOptions);
        joinableDropdown.AddOptions(joinableOptions);
    }

    public void OnClick()
    {
        //Debug.Log(MaxPlayers.text);
        CreateLobbyPOST newLobby = new CreateLobbyPOST();
        //Debug.Log(gameType + " " + isJoinable + " " + MaxPlayers.text); 
        newLobby.CreateLobby(gameType, isJoinable, Int32.Parse(MaxPlayers.text));
        Debug.Log("Lobby created"); 
    }


}