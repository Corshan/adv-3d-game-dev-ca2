using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _quest;
    [SerializeField] private GameObject _inventory;
    [SerializeField] public GameObject _dialogue;

    void Start()
    {
        _dialogue.SetActive(false);
        _inventory.SetActive(false);
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _quest.SetActive(!_quest.active);
            _inventory.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventory.SetActive(!_inventory.active);
            _quest.SetActive(false);
        }
    }
}
