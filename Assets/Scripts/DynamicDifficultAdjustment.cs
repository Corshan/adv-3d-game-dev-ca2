using Unity.AI.Navigation;
using UnityEngine;

namespace Week8C
{
    public class DynamicGameplay : MonoBehaviour
    {
        public GameObject[] allNpcs;
        public float npcSpeed, npcHearingRadius;
        [SerializeField]
        [Range(1, 3)]
        public int currentDifficultyLevel = 1;

        public float hpCheckFrequency, hpCheckTimer;
        public float checkUserProgressTimer, checkUserProgressTimePeriod;
        public bool userIsHighlySkilled;
        public int userSkillLevel = 5;
        public GameObject[] allBridge1, allBridge2;
        public float bridgeSize;
        public float npcSpawnPeriod;
        public float hpCheckPeriod;
        public NavMeshSurface navMeshSurface;
        void Start()
        {
            ChangeDifficulty();
        }

        void ChangeNpcSpeed()
        {
            allNpcs = GameObject.FindGameObjectsWithTag("NPC");

            for (int i = 0; i < allNpcs.Length; i++)
            {
                allNpcs[i].GetComponent<ControlNPCDynamic>().ChangeSpeed(npcSpeed);
            }
        }

        void ChangeNpcHearingRadius()
        {
            allNpcs = GameObject.FindGameObjectsWithTag("NPC");

            for (int i = 0; i < allNpcs.Length; i++)
            {
                allNpcs[i].GetComponent<ControlNPCDynamic>().ChangeHearingDistance(npcHearingRadius);
            }
        }

        void ChangeDifficulty()
        {
            switch (currentDifficultyLevel)
            {
                case 1:
                    npcSpeed = 2.5f;
                    npcHearingRadius = 3.0f;
                    bridgeSize = 20f;
                    npcSpawnPeriod = 60;
                    hpCheckPeriod = 10;
                    break;
                case 2:
                    npcSpeed = 4.5f;
                    npcHearingRadius = 7.0f;
                    bridgeSize = 15f;
                    npcSpawnPeriod = 30;
                    hpCheckPeriod = 20;
                    break;
                case 3:
                    npcSpeed = 6.5f;
                    npcHearingRadius = 10.0f;
                    bridgeSize = 10f;
                    npcSpawnPeriod = 10;
                    hpCheckPeriod = 30;
                    break;

            }
            ChangeNpcSpeed();
            ChangeNpcHearingRadius();
            ChangeBridge1();
            ChangeBridge2();
            ChangeNPCSpawnPeriod();
            ChangeHPacksSpawnPeriod();
        }

        void ChangeHPacksSpawnPeriod()
        {
            GameObject.Find("SpawnHealthPacks").GetComponent<SpawnHealthPacks>().HPCheckPeriod = hpCheckPeriod;
        }

        void ChangeNPCSpawnPeriod()
        {
            GameObject.Find("SpawnNPC").GetComponent<SpawnNPCs>().spawnPeriod = npcSpawnPeriod;
        }

        void CheckUserProgress()
        {
            if (userSkillLevel < 3)
            {
                currentDifficultyLevel = 1;
            }
            else if (userSkillLevel < 7)
            {
                currentDifficultyLevel = 2;
            }
            else
            {
                currentDifficultyLevel = 3;
            }
            ChangeDifficulty();
        }

        public void ChangeBridge1()
        {
            allBridge1 = GameObject.FindGameObjectsWithTag("Bridge1");
            for (int i = 0; i < allBridge1.Length; i++)
            {
                allBridge1[i].transform.localScale = new Vector3(allBridge1[i].transform.localScale.x, 1f, bridgeSize);
            }
        }

        public void ChangeBridge2()
        {
            allBridge2 = GameObject.FindGameObjectsWithTag("Bridge2");
            for (int i = 0; i < allBridge2.Length; i++)
            {
                allBridge2[i].transform.localScale = new Vector3(bridgeSize, 1f, allBridge2[i].transform.localScale.z);
            }
        }

        void Update()
        {
            checkUserProgressTimer += Time.deltaTime;
            if (checkUserProgressTimer >= checkUserProgressTimePeriod)
            {
                userSkillLevel++;
                checkUserProgressTimer = 0;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                npcSpeed = 5.0f;
                ChangeNpcSpeed();
                npcHearingRadius = 10.0f;
                ChangeNpcHearingRadius();

            }


            if (Input.GetKeyDown(KeyCode.O))
            {
                npcSpeed = 3.5f;
                ChangeNpcSpeed();
                npcHearingRadius = 5.0f;
                ChangeNpcHearingRadius();
            }
            CheckUserProgress();

        }

        public void ChangePlayerSkillLevel(int i, string zone)
        {
            userSkillLevel += i;
            if (userSkillLevel < 1) userSkillLevel = 1;
            if (userSkillLevel > 10) userSkillLevel = 10;

            print("Player skill level is now: " + userSkillLevel + " after entering " + zone);
        }
    }
}
