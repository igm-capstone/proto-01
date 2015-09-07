using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public GameObject hudPrefab;

    private Slider slider;
    private Image playIcon;
    private Image recIcon; 
    private Text playCounter;

    private int playCount = 0;


	// Use this for initialization
	void Awake () {
        Text label = hudPrefab.transform.FindChild("PlayerLabel").GetComponent<Text>();
        label.text = gameObject.name;

        slider = hudPrefab.GetComponentInChildren<Slider>();
        playIcon = hudPrefab.transform.FindChild("Play").GetComponent<Image>();
        recIcon = hudPrefab.transform.FindChild("Record").GetComponent<Image>();
        playCounter = hudPrefab.transform.FindChild("PlayCount").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
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


}
