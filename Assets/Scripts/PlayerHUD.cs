using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    public GameObject hudPrefab;

    private Slider healthSlider;
    private Text recordingCountText, P1Score, P2Score, Timer;   
    private int recordingsCount, p1ScoreCount, p2ScoreCount;
    public float TimerInMins = 5;

    private int playCount = 0;


    // Use this for initialization
    void Awake()
    {
        Text label = hudPrefab.transform.FindChild("PlayerLabel").GetComponent<Text>();
        label.text = gameObject.name;

        healthSlider = hudPrefab.GetComponentInChildren<Slider>();
        
        recordingCountText = hudPrefab.transform.FindChild("RecordingCount").GetComponent<Text>();
        recordingsCount = 0;
        recordingCountText.text = "Recordings: " + recordingsCount.ToString();

        P1Score = transform.FindChild("Score").transform.FindChild("Player1Score").GetComponent<Text>();
        P2Score = transform.FindChild("Score").transform.FindChild("Player2Score").GetComponent<Text>();
        Timer = transform.FindChild("Score").transform.FindChild("Timer").GetComponent<Text>();

        Timer.text = TimerInMins.ToString() + ":00";
        TimerInMins *= 60;
    }

    public void setSlider(float value)
    {
        healthSlider.value = value;
    }

    public void stopRec(int recording)
    {
        //Update the number of recordings in play
        recordingsCount += recording;
        recordingCountText.text = "Recordings: " + recordingsCount.ToString();
    }

    public void SetP1Score(int value)
    {
        p1ScoreCount += value;
        P1Score.text = p1ScoreCount.ToString();
    }

    public void SetP2Score(int value)
    {
        p2ScoreCount += value;
        P2Score.text = p2ScoreCount.ToString();
    }

    void Update()
    {
        TimerInMins -= Time.deltaTime;
        Timer.text = ((int)(TimerInMins / 60)).ToString() + ":" + ((int)(TimerInMins % 60)).ToString("00");
    }
}
