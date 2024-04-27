using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private int _currentStageId;
    private List<Quest> _quests;
    private string _stageTitle;
    private string _stageDescription;

    private void Start()
    {
        _quests = new List<Quest>();
        LoadQuests();

        foreach (var q in _quests)
        {
            print($"{q.Action} {q.Target} {q.XP} {q.IsComplete}");
        }
    }

    private void LoadQuests()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("quest");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode stage in doc.SelectNodes("quest/stage"))
        {
            int stageID = int.Parse(stage.Attributes.GetNamedItem("id").Value);
            
            if (stageID == _currentStageId) continue;

            _stageTitle = stage.Attributes.GetNamedItem("name").Value;
            _stageDescription = stage.Attributes.GetNamedItem("description").Value;

            foreach (XmlNode results in stage)
            {
                foreach (XmlNode result in results)
                {
                    string action = result.Attributes.GetNamedItem("action").Value;
                    string target = result.Attributes.GetNamedItem("target").Value;
                    int xp = int.Parse(result.Attributes.GetNamedItem("xp").Value);

                    _quests.Add(new Quest(action, target, xp));
                }
            }
        }
    }
}
