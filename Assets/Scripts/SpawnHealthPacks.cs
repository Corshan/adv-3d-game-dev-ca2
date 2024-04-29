using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthPacks : MonoBehaviour
{
    public GameObject[] spawnLocations;
    GameObject[] allHPs;
    public GameObject healthPack;
    bool[] HPLocationEmpty;
    GameObject player;
    float timer;
    public float HPCheckPeriod = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            GameObject hp = Instantiate(healthPack, spawnLocations[i].transform.position + Vector3.up * 0.2f, Quaternion.identity);
        }
    }

    void CheckHealthPacks()
    {
        if (anyHPAvailable()) return;
        else SpawnPacks();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > HPCheckPeriod)
        {
            timer = 0;
            CheckHealthPacks();
        }
    }

    bool anyHPAvailable()
    {
        allHPs = GameObject.FindGameObjectsWithTag("HealthPack");

        return allHPs.Length > 0;
    }

    void SpawnPacks()
    {
        int randomNum = Random.Range(0, spawnLocations.Length);

        GameObject hp = Instantiate(healthPack, spawnLocations[randomNum].transform.position + Vector3.up * 0.2f, Quaternion.identity);
    }
}
