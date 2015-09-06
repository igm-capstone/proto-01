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
    private bool trapBool = false;
    private int switchNum;

    void Start()
    {
        floorBaseMaterial = floors[1].GetChild(1).GetComponent<Renderer>().material;
        origFlashColor = floorFlashMaterial.color;
    }


    void Update()
    {
        if (trapBool)
        {
            float t = (Mathf.PingPong(Time.time * 3, 1.0f));
            floorFlashMaterial.color = Color.Lerp(floorBaseMaterial.color, floorFlashColor, t);
        }
    }

    IEnumerator activateFlash(float flashTime)
    {        
        //yield return new WaitForSeconds(flashTime);
        foreach (Transform floor in floors[switchNum])
        {
            floor.gameObject.GetComponent<Renderer>().material = floorFlashMaterial;
        }
        trapBool = true;
        
        yield return new WaitForSeconds(flashTime);
        trapBool = false;
        floorFlashMaterial.color = floorBaseMaterial.color;
        
        StartCoroutine("activateTrap", flashTime);
    }

    IEnumerator activateTrap(float flashTime)
    {
        //turn off the respective floors
        foreach(Transform floor in floors[switchNum])
        {
            floor.gameObject.SetActive(false);
        }

        //wait for some time
        yield return new WaitForSeconds(flashTime);
        foreach (Transform floor in floors[switchNum])
        {
            floor.gameObject.SetActive(true);
        }
    }

    public void trapControl(string switchName)
    {
        switch (switchName)
        {
            case "Top":
                switchNum = 0;
                StartCoroutine("activateFlash", flashTime);
                break;
            case "Left":
                switchNum = 1;
                StartCoroutine("activateFlash", flashTime);
                break;
            case "Bottom":
                switchNum = 2;
                StartCoroutine("activateFlash", flashTime);
                break;
            case "Right":
                switchNum = 3;
                StartCoroutine("activateFlash", flashTime);
                break;
            default:
                break;
        }
    }
}