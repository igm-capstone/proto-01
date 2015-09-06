using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    Transform[] floors = new Transform[4];
    [SerializeField]
    private float trapDelay;
    [SerializeField]
    private Material floorFlashMaterial;
    [SerializeField]
    private Color floorFlashColor;
    private Color origFlashColor;
    private Material floorBaseMaterial;
    private bool trapBool = false;

    void Start()
    {
        floorBaseMaterial = floors[1].GetChild(1).GetComponent<Renderer>().material;
        origFlashColor = floorFlashMaterial.color;
        StartCoroutine("activateFlash", trapDelay);
    }

    IEnumerator activateFlash(float trapDelay)
    {        
        yield return new WaitForSeconds(trapDelay);
        foreach (Transform floor in floors[0])
        {
            floor.gameObject.GetComponent<Renderer>().material = floorFlashMaterial;
        }
        trapBool = true;
        
        yield return new WaitForSeconds(trapDelay);
        trapBool = false;
        floorFlashMaterial.color = floorBaseMaterial.color;
        
        StartCoroutine("activateTrap", trapDelay);
    }

    void Update()
    {
        if (trapBool)
        {
            float t = (Mathf.PingPong(Time.time * 3, 1.0f));
            floorFlashMaterial.color = Color.Lerp(floorBaseMaterial.color, floorFlashColor, t);
        }
    }

    IEnumerator activateTrap(float trapDelay)
    {
        //turn off the respective floors
        foreach(Transform floor in floors[0])
        {
            floor.gameObject.SetActive(false);
        }

        //wait for some time
        yield return new WaitForSeconds(trapDelay);
        foreach (Transform floor in floors[0])
        {
            floor.gameObject.SetActive(true);
           // floor.gameObject.GetComponent<Renderer>().material = floorBaseMaterial;
        }
       
        //start the flash coroutine
        StartCoroutine("activateFlash", trapDelay);
    }
}