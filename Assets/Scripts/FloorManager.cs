using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    Transform[] floors = new Transform[4];
    [SerializeField]
    private float flashTime;
    [SerializeField]
    private Material floorFlashMaterial;
    [SerializeField]
    private Color floorFlashColor;
    private Color origFlashColor;
    private Material floorBaseMaterial;
    private bool flashBool = false;
    private bool timerBool = false;
    private int switchNum = 0;

    public float timer = 3.0f;
    void Start()
    {
        floorBaseMaterial = floors[1].GetChild(1).GetComponent<Renderer>().material;
        floorFlashMaterial.color = floorBaseMaterial.color;
        origFlashColor = floorFlashMaterial.color;
        foreach (Transform floor in floors)
        {
            floor.gameObject.SetActive(false);
        }
    }


    void Update()
    {

        if (timerBool == true)
        {
            if (timer >= 0)
            {
                timer = timer - Time.deltaTime;
            }
            else if (timer <= 0)
            {
                floors[switchNum].gameObject.SetActive(false);
                gameObject.transform.FindChild("FloorBaseTop").gameObject.SetActive(true);
                floorFlashMaterial.color = floorBaseMaterial.color;        

                timerBool = false;
                timer = 3.0f;
            }
        }

        if (flashBool)
        {
            float t = (Mathf.PingPong(Time.time * 3, 1.0f));
            floorFlashMaterial.color = Color.Lerp(floorBaseMaterial.color, floorFlashColor, t);
        }
    }

    IEnumerator activateTrap(float flashDuration)
    {     
        //turn on the respective floors
        floors[switchNum].gameObject.SetActive(true);
        flashBool = true;

        yield return new WaitForSeconds(flashDuration);
        gameObject.transform.FindChild("FloorBaseTop").gameObject.SetActive(false);
        flashBool = false;
        timerBool = true;
    }

	public void spawnFloors()
	{
		floors[switchNum].gameObject.SetActive(false);
		gameObject.transform.FindChild("FloorBaseTop").gameObject.SetActive(true);
		floorFlashMaterial.color = floorBaseMaterial.color;        
		
		timerBool = false;
		timer = 3.0f;
	}

    public void trapControl(string switchName)
    {
        switch (switchName)
        {
            case "Top":
                switchNum = 0;
                StartCoroutine(activateTrap(flashTime));
                break;
            case "Left":
                switchNum = 1;
                StartCoroutine(activateTrap(flashTime));
                break;
            case "Bottom":
                switchNum = 2;
                StartCoroutine(activateTrap(flashTime));
                break;
            case "Right":
                switchNum = 3;
                StartCoroutine(activateTrap(flashTime));
                break;
            default:
                break;
        }
    }
}