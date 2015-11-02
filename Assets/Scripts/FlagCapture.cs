using UnityEngine;
using System.Collections;

public enum FlagState { atBase, withEnemy, dropped };
public enum PlayerWhoTakesFlag { RedPlayer, BluePlayer };

public class FlagCapture : MonoBehaviour 
{    
    public FlagState myFlagState;
    [SerializeField]
    private PlayerWhoTakesFlag otherPlayer;
    private GameObject otherFlag;
    private Transform myBase;
    private Vector3 myFlagStartPos;
    public string myColor;
    private PlayerHUD playerHUD;
    
    void Awake()
    {
        playerHUD = GameObject.Find("Canvas").GetComponent<PlayerHUD>();
        myFlagState = FlagState.atBase;
        if (otherPlayer.ToString() == "RedPlayer")
        {
            otherFlag = GameObject.Find("RedFlag");
            myBase = GameObject.Find("BlueBase").transform;
            myColor = "Blue";
        }
        else
        {
            otherFlag = GameObject.Find("BlueFlag");
            myBase = GameObject.Find("RedBase").transform;
            myColor = "Red";
        }

        myFlagStartPos = transform.position;
    }

    void Update()
    {
        //TODO: Change this to when the player or recording who has the flag, dies; cahnge state to dropped;
        if(Input.GetKeyDown(KeyCode.F))
        {
            transform.parent = otherFlag.GetComponent<FlagCapture>().myBase;
            transform.position += new Vector3(2, 0, 2);
            myFlagState = FlagState.dropped;
        }
    }

    /*
     same player | same flag || if not at base, return the flag to base
     same player | other flag || if ( blue player brings it to the blue base, check if the blue flag is present, if yes, return the red flag to its base, add a point for blue) else (collect it )
    */
    public void OnTriggerEnter(Collider other)
    {        
        //this flag
        if (other.tag == otherPlayer.ToString())
        {
            switch (myFlagState)
            {
                case FlagState.atBase:
                    transform.parent = other.gameObject.transform;
                    myFlagState = FlagState.withEnemy;
                    break;
                case FlagState.withEnemy:
                    break;
                case FlagState.dropped:
                    transform.parent = other.gameObject.transform;
                    myFlagState = FlagState.withEnemy;
                    break;
                default:
                    break;
            }
        }

        if(myFlagState == FlagState.atBase)
        {
            if(other.tag == myColor+"Player")
            {
                if(otherFlag.GetComponent<FlagCapture>().myFlagState == FlagState.withEnemy)
                {
                    //return the other flag
                    otherFlag.transform.parent = otherFlag.GetComponent<FlagCapture>().myBase;
                    otherFlag.transform.position = otherFlag.GetComponent<FlagCapture>().myFlagStartPos;
                    //    //TODO
                    //    //add 1 point to this color player
                    if (myColor == "Blue")
                        playerHUD.SetP1Score(1);
                    else if (myColor == "Red")
                        playerHUD.SetP2Score(1);
                    otherFlag.GetComponent<FlagCapture>().myFlagState = FlagState.atBase;
                }
            }
        }

        if (myFlagState == FlagState.dropped)
        {
            if (other.tag == myColor + "Player")
            {
                transform.parent = myBase;
                transform.position = myFlagStartPos;
                myFlagState = FlagState.atBase;
            }
        }
    }
}
