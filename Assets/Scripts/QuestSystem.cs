using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private int _currentStageId;
    private List<Quest> _quests;
    public List<Quest> Quests => _quests;
    private string _stageTitle;
    private string _stageDescription;

    private void Start()
    {
        _quests = new List<Quest>();
        LoadQuests();

        foreach (var q in _quests)
        {
            print($"{q.Action} {q.Target} {q.XP} {q.IsComplete} {q.Type}");
        }
    }

    private void KillQuest(Quest quest)
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC").ToList();

        if (npcs.Count <= 0) quest.CompleteQuest();
    }

    private void CollectQuest(Quest quest)
    {
        var items = GameObject.FindGameObjectsWithTag("Collect").ToList();

        if (items.Count <= 0) quest.CompleteQuest();
    }

    private void TalkTo(Quest quest)
    {
        var npc = GameObject.Find($"{quest.Target}Temp");

        if (npc == null) quest.CompleteQuest();
    }

    private void GoTo(Quest quest)
    {
        var location = GameObject.Find($"{quest.Target}Temp");

        if (location == null) quest.CompleteQuest();
    }

    private void Update()
    {
        foreach (var quest in _quests)
        {
            switch (quest.Type)
            {
                case Quest.QuestType.KILL:
                    KillQuest(quest);
                    break;
                case Quest.QuestType.COLLECT:
                    CollectQuest(quest);
                    break;
                case Quest.QuestType.TALK_TO:
                    TalkTo(quest);
                    break;
                case Quest.QuestType.GO_TO:
                    GoTo(quest);
                    break;
            }
        }
    }

    private void LoadQuests()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("quest");
        XmlDocument doc = new();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode stage in doc.SelectNodes("quest/stage"))
        {
            int stageID = int.Parse(stage.Attributes.GetNamedItem("id").Value);

            if (stageID == _currentStageId)
            {
                _stageTitle = stage.Attributes.GetNamedItem("name").Value;
                _stageDescription = stage.Attributes.GetNamedItem("description").Value;

                foreach (XmlNode results in stage)
                {
                    foreach (XmlNode result in results)
                    {
                        string action = result.Attributes.GetNamedItem("action").Value;
                        string target = result.Attributes.GetNamedItem("target").Value;
                        int xp = int.Parse(result.Attributes.GetNamedItem("xp").Value);

                        Quest.QuestType type = (Quest.QuestType)int.Parse(result.Attributes.GetNamedItem("type").Value);

                        _quests.Add(new Quest(action, target, xp, type));
                    }
                }
            }
        }
    }
}
