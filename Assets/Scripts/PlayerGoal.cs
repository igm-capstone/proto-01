using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGoal : MonoBehaviour {

    public Text victoryText;

    [SerializeField]
    [Range(1, 2)]
    short reqPlayerId = 1;
    public GameObject nextLevel, restartLevel;
    public Color blueVictoryTextColor;

    void Awake()
    {
        // Disable Victory Text at the star of the game.
        victoryText.enabled = false;
    }

    void OnTriggerEnter(Collider Other)
    {
        // Chekc if Object Collided is the player
        if(Other.tag == "Player")
        {
            if (Other.GetComponent<PlayerController>().playerId == reqPlayerId)
            {
                if (reqPlayerId == 1)
                {
                    victoryText.text = "Blue Player Wins!";
                    victoryText.color = blueVictoryTextColor;
                }
                else if (reqPlayerId == 2)
                {
                    victoryText.text = "Red Player Wins!";
                    victoryText.color = new Color(1, 0, 0);
                }
                victoryText.enabled = true;

                nextLevel.SetActive(true);
                restartLevel.SetActive(true);

            }
        }

    }   

    public void DisplayWinner(string playerName)
    {
        if (playerName == "Player 01")
        {
            victoryText.text = "Red Player Wins!";
            victoryText.color = new Color(1, 0, 0); 
        }
        else if (playerName == "Player 02")
        {
            victoryText.text = "Blue Player Wins!";
            victoryText.color = blueVictoryTextColor;
        }
        victoryText.enabled = true;
    }
}
