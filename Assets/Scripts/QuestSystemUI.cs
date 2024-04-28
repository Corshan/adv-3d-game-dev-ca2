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
            if (quest.IsComplete) continue;

            str += $"{quest.Action} {quest.Target} [{quest.XP} XP] ";

            switch (quest.Type)
            {
                case Quest.QuestType.COLLECT:
                    var items = GameObject.FindGameObjectsWithTag("Collect");
                    str += $"{10-items.Length}/10";
                    break;
                case Quest.QuestType.KILL:
                    var npcs = GameObject.FindGameObjectsWithTag("NPC");
                    str += $"{5-npcs.Length}/5";
                    break;
            }

            str += "\n";
        }

        _questInfo.text = str;
    }
}
