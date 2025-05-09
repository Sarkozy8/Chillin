using System;
using System.Collections;
using UnityEngine;

public class SightingManager : MonoBehaviour
{
    public int randomDeer = 5;

    public GameObject deer1;
    public GameObject deer2;
    public GameObject deer3;
    public GameObject deer4;
    public GameObject deer5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //randomDeer = UnityEngine.Random.Range(1, 6);
        StartCoroutine(DeerSelector());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DeerSelector()
    {
        yield return new WaitForSeconds(2);

        switch (randomDeer)
        {
            case 1:
                deer1.SetActive(true);
                break;
            case 2:
                deer2.SetActive(true);
                break;
            case 3:
                deer3.SetActive(true);
                break;
            case 4:
                deer4.SetActive(true);
                break;
            case 5:
                deer5.SetActive(true);
                break;
        }
    }
}
