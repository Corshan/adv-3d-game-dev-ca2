using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] private QuestSystem _questSystem;
    [SerializeField] private TextMeshProUGUI _questInfo;

    void Update()
    {
        string str = "";
        foreach (var quest in _questSystem.Quests)
        {
            if(quest.IsComplete) continue;

            str +=  $"{quest.Action} {quest.Target} [{quest.XP} XP]\n";
        }

        _questInfo.text = str;
    }
}
