using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { set; get; }
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this; 
        DontDestroyOnLoad(gameObject); 
    }

    // connect button
    public void ConnectButton()
    {
        Debug.Log("Connect");
    }

    // host button
    public void HostButton()
    {
        Debug.Log("Host"); 
    }

    // button to display lobbies
    public void LobbyListButton()
    {
        Debug.Log("list of lobbies");
    }


}
