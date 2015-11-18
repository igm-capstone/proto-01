using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGoal : MonoBehaviour {

    public Text victoryText;

    [SerializeField]
    [Range(1, 2)]
    short reqPlayerId = 1;
    public GameObject nextLevel, restartLevel;

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
                    victoryText.color = new Color(0, 0, 255);
                }
                else if (reqPlayerId == 2)
                {
                    victoryText.text = "Red Player Wins!";
                    victoryText.color = new Color(255f,0,0);
                }
                victoryText.enabled = true;

                nextLevel.SetActive(true);
                restartLevel.SetActive(true);

            }
        }

    }
}
