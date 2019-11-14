using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLobby : MonoBehaviour
{
    public Text curPlayers;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;
    // Start is called before the first frame update
    //void Start()
    //{
    //    // display the current players in the lobby 
    //    Debug.Log(lobbyID); 
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // update the text to show all the current ready statuses 
    //}

    public void SetLobbyID(int LobbyID)
    {
        LobbyIDText.text = "LobbyID: " + LobbyID.ToString();
        lobbyID = LobbyID;
    }

}
