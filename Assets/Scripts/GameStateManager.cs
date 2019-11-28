using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fragsurf.Movement;
using UnityEngine.UI; 
public class GameStateManager : MonoBehaviour
{

    private bool winnerFound = false;
    public static int maxPlayers; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GameState", 0f, 1f);
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
                string name = player.transform.GetChild(1).GetComponent<TextMesh>().text;
                int lives = player.GetComponent<SurfCharacter>().getLives();
                if (lives > 0)
                {
                    winnerName = name;
                }
                else if (lives == 0)
                {
                    deadPlayers++;
                }
            }
            if (players.Length == maxPlayers)
            {
                winnerFound = deadPlayers == (players.Length - 1);
                if (winnerFound)
                {
                    foreach (GameObject player in players)
                    {
                        player.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = winnerName + ", Wins!";
                    }
                }

            }
        }
    }
}