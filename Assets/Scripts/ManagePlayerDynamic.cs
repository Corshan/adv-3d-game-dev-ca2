using System;
using UnityEngine;
using Week8C;

public class ManagePlayerDynamic : MonoBehaviour
    {
        public Vector3 playerInitPosition;
        void Start()
        {
            playerInitPosition = transform.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Checkpoint"))
            {
                GameObject.Find("DynamicGamePlay").GetComponent<DynamicGameplay>().ChangePlayerSkillLevel(+1, "Checkpoint");
                Destroy(other.collider.gameObject);
            }

            if (other.collider.CompareTag("Deadzone"))
            {
                transform.position = playerInitPosition;
                GameObject.Find("DynamicGamePlay").GetComponent<DynamicGameplay>().ChangePlayerSkillLevel(-1, "Deadzone");
                Destroy(other.collider.gameObject);
                GetComponent<Collider>().enabled = false;
                transform.position = playerInitPosition;
                GetComponent<Collider>().enabled = true;
            }
        }

        void Update()
        {
        
        }
    }
