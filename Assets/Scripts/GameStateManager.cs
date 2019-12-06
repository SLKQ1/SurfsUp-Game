using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fragsurf.Movement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{

    public static bool winnerFound;
    public static int maxPlayers; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GameState", 5f, 1f);
    }

    private void GameState()
    {


        string winnerName = "Player";
        int deadPlayers = 0;
        

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (!winnerFound)
        {

            foreach (GameObject player in players)
            {
                string name = player.transform.GetChild(1).GetComponent<TextMeshPro>().text;
                int lives = player.GetComponent<SurfCharacter>().lives;
                if (lives > 0)
                {
                    winnerName = name;
                }
                else if (lives == 0)
                {
                    deadPlayers++;
                }
            }
            if (players.Length == maxPlayers && players.Length != 1)
            {
                winnerFound = deadPlayers == (players.Length - 1);
                if (winnerFound)
                {
                    foreach (GameObject player in players)
                    {
                        player.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = winnerName + ", Wins!";
                        player.transform.GetChild(3).GetChild(4).GetComponent<Image>().enabled = true;
                        player.transform.GetChild(3).GetChild(4).GetComponent<Button>().enabled = true;
                        player.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().enabled = true; 

                    }
                }

            } else if (players.Length == 1)
            {
                if (players[0].GetComponent<SurfCharacter>().lives == 0)
                {
                    winnerFound = true;
                    players[0].transform.GetChild(3).GetChild(3).GetComponent<Text>().text = "You, Win!";
                    players[0].transform.GetChild(3).GetChild(4).GetComponent<Image>().enabled = true;
                    players[0].transform.GetChild(3).GetChild(4).GetComponent<Button>().enabled = true;
                    players[0].transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Text>().enabled = true;
                }
            }
        }
    }
    
    public void Disconnect()
    {
        // shutdown client
        Mirror.NetworkClient.Disconnect();
        Mirror.NetworkClient.Shutdown();
  
        // change scene 
        SceneManager.LoadScene(0);

    }
}