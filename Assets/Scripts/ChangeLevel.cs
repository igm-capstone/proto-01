using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour 
{
    public void NextLevel()
    {       
        Application.LoadLevel("CircularFloorTrap");
    }

    public void RestartLevel()
    {
        Application.LoadLevel("BlocksAndLadders");
    }
}
