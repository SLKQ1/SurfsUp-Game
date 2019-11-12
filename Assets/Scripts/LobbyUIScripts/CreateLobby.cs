using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobby : MonoBehaviour
{

    List<string> gameTypeOptions = new List<string>() { "Please select","Solo", "Multiplayer" };
    List<string> joinableOptions = new List<string>() { "Please select", "True", "False" };

    public Dropdown gameTypeDropdown;
    public Dropdown joinableDropdown;

    public void Dropdown_IndexChanged_GameType(int index)
    {
        Debug.Log(gameTypeOptions[index]); 
    }
    public void Dropdown_IndexChanged_JoinableOptions(int index)
    {
        Debug.Log(joinableOptions[index]); 
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

    // function for create lobby button 
    public void OnClick()
    {

    }
}
