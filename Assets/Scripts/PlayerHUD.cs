using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    public GameObject hudPrefab;

    private Slider slider;
    private Image playIcon;
    private Image recIcon;
    private Image heartIcon;
    private Text playCounter;
    private Text livesCount;

    private int playCount = 0;


    // Use this for initialization
    void Awake()
    {
        Text label = hudPrefab.transform.FindChild("PlayerLabel").GetComponent<Text>();
        label.text = gameObject.name;

        slider = hudPrefab.GetComponentInChildren<Slider>();
        playIcon = hudPrefab.transform.FindChild("Play").GetComponent<Image>();
        recIcon = hudPrefab.transform.FindChild("Record").GetComponent<Image>();
        heartIcon = hudPrefab.transform.FindChild("Heart").GetComponent<Image>();
        playCounter = hudPrefab.transform.FindChild("PlayCount").GetComponent<Text>();
        livesCount = hudPrefab.transform.FindChild("LivesCount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSlider(float value)
    {
        slider.value = value;
    }

    public void startRec()
    {
        recIcon.color = Color.red;
    }

    public void stopRec()
    {
        recIcon.color = Color.white;
    }

    public void setPlaybackCounter(int value)
    {
        playCount = value;
        playCounter.text = playCount.ToString();


        if (playCount > 0)
            playIcon.color = Color.green;
        else
        {
            playIcon.color = Color.white;
            setSlider(0);
        }
    }

    public void setLives(int t)
    {
        if (t == -1)
        {
            livesCount.text = "X";
        }
        else
            livesCount.text = t.ToString();

        if (t > 0 || t == -1)
            heartIcon.color = Color.red;
        else
        {
            heartIcon.color = Color.gray;
        }
    }


}
