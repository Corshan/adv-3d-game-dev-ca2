using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _quest;

    [System.Obsolete]
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) _quest.SetActive(!_quest.active);
    }
}