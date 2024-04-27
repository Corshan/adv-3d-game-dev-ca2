using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest
{
    private string _action;
    public string Action => _action;
    private string _target;
    public string Target => _target;
    private int _xp;
    public int XP => _xp;
    private bool _isComplete;
    public bool IsComplete => _isComplete;
    private QuestType _type;
    public QuestType Type => _type;
    public enum QuestType {
        NONE,
        KILL,
        COLLECT,
        GO_TO,
        TALK_TO,
    }

    public Quest(string action, string target, int xp, QuestType type)
    {
        _action = action;
        _target = target;
        _xp = xp;
        _type = type;
        _isComplete = false;
    }

    public void CompleteQuest() => _isComplete = true;
}
