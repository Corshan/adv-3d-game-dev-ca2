using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week8C;

public class SpawnNPCs : MonoBehaviour
{
    public float spawnPeriod;
    float timer;
    public GameObject npc;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        spawnPeriod = 10;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnPeriod)
        {
            timer = 0;
            go = Instantiate(npc, transform.position, Quaternion.identity);
            go.tag = "NPC";

            float newHearingDistance = GameObject.Find("DynamicGamePlay").GetComponent<DynamicGameplay>().npcHearingRadius;
            go.GetComponent<ControlNPCDynamic>().ChangeHearingDistance(newHearingDistance);
        }
    }
}
